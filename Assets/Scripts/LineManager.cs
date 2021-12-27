using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineManager : MonoBehaviour
{
    Storage storage;
    public Movement SnakeStats;
    private System.Random random;
    public GameObject CubePrefab;
    public Vector3 LineStartPosition;
    public float CubeOffset;
    public GameObject FoodPrefab;
    public Vector3 FoodStartPosition;
    public float FloorWidth = 5;
    public float LineVelocity = 0.3f;
    public float OffscreenZ = -2.3f;
    public TailController tailController;
    public List<Cube> CollidingCubes = new List<Cube>();
    public float ShorteningTime = 0.5f;
    private float ShorteningTimer;
    public float DistanceBetweenLines = 6.5f;
    public int BlocksBroke;
    public int BlocksToWin = 10;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject RepeatButton;
    public TMPro.TextMeshProUGUI Score;
    public TMPro.TextMeshProUGUI Lvl;
    private int LvlNum = 1;

    public enum GameState
    {
        normal,
        collision,
        win,
        lose,
        pause
    }

    public GameState gameState = GameState.normal;
    public void GenLine() 
    {
        int smallBlockMax = SnakeStats.Length + storage.GetLineBonus(1) - storage.GetLineDurability(1) + storage.GetLineBonus(0) - storage.GetLineDurability(0);
        if (smallBlockMax <= 0)
        {
            smallBlockMax = 0;
        }
        int durability = random.Next(0, smallBlockMax);
        int[] LineBlock = new int[4];
        for (int i = 0; i < LineBlock.Length; i++)
        {
            LineBlock[i] = random.Next(smallBlockMax / 2, smallBlockMax * 3);
        }

        int position = random.Next(0, 5);
        int[] LinePosition = new int[5];
        for (int i = 0; i < position; i++)
        {
            LinePosition[i] = LineBlock[i];
        }

        LinePosition[position] = durability;
        for (int i = position; i < LineBlock.Length; i++)
        {
            LinePosition[i + 1] = LineBlock[i];
        }

        // остановочка. Следующая станция - генерация линии.
        int balz = random.Next(1, 4); //"еда"
        int[] LineBalz = new int[balz];
        for (int i = 0; i < LineBalz.Length; i++)
        {
            int BalzNum = random.Next(1, 10);
            LineBalz[i] = BalzNum;
        }

        Line LineOrder = new Line(LinePosition, LineBalz);
        storage.AddLine(LineOrder);

    }

    void Start()
    {
        storage = new Storage();
        random = new System.Random();
        //--Test Code! ALYARMA!!!-- OMG! REAL CODE ALARM!
        LevelStart();
    }

    public void Restart()
    {
        CollidingCubes.Clear();
        
        Destroy(storage.storage[0].LineHub.gameObject);
        Destroy(storage.storage[1].LineHub.gameObject);
        Destroy(storage.storage[2].LineHub.gameObject);
        if (gameState == GameState.lose)
        {
            tailController.SetLength(10);
        }
        storage.storage[0] = null;
        storage.storage[1] = null;
        storage.storage[2] = null;
        LevelStart();
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        BlocksBroke = 0;
        Score.text = BlocksToWin.ToString();
        gameState = GameState.normal;
    }
    public void LevelStart()
    {
        GenLine();
        GenTrail(0);
        GenLine();
        GenTrail(0);
        GenLine();
        GenTrail(0);
        storage.storage[1].LineHub.Translate(0, 0, -DistanceBetweenLines);
        storage.storage[2].LineHub.Translate(0, 0, -2 * DistanceBetweenLines);

    }

    private void FixedUpdate()
    {
        switch (gameState)
        {
            case GameState.normal:
                for (int i = 0; i < storage.storage.Length; i++)
                {
                    storage.storage[i].LineHub.Translate(0, 0, -LineVelocity);

                }

                if (storage.storage[2].LineHub.position.z <= OffscreenZ)
                {
                    Transform DestroyedLine = storage.storage[2].LineHub;
                    GenLine();
                    GenTrail(0);
                    Destroy(DestroyedLine.gameObject);
                }
                break;
            case GameState.collision:
                if (ShorteningTimer >= ShorteningTime)
                {
                    List<int> CubesToRemove = new List<int>();
                    for (int i = 0; i < CollidingCubes.Count; i++)
                    {
                        tailController.RemoveCircle();
                        if (SnakeStats.Length <= 0)
                        {
                            gameState = GameState.lose; //Lose.
                            LoseScreen.SetActive(true);

                            return;
                        }
                        CollidingCubes[i].CubeDurability--;
                        CollidingCubes[i].CubeDurabilityText.text = CollidingCubes[i].CubeDurability.ToString();
                        
                        if (CollidingCubes[i].CubeDurability <= 0)
                        {
                            CubesToRemove.Add(i);
                        }
                    }

                    for (int i = 0; i < CubesToRemove.Count; i++)
                    {
                        Destroy(CollidingCubes[CubesToRemove[i]].gameObject);
                        CollidingCubes.RemoveAt(CubesToRemove[i]);
                        BlocksBroke++;
                        Score.text = (BlocksToWin - BlocksBroke).ToString();
                    }
                    if (BlocksBroke >= BlocksToWin)
                    {
                        gameState = GameState.win; //Win.
                        WinScreen.SetActive(true);
                        BlocksToWin += 10;
                        LvlNum++;
                        Lvl.text = LvlNum.ToString();
                        Score.text = BlocksToWin.ToString();
                        return;
                    }
                    if (CollidingCubes.Count == 0)
                    {
                        gameState = GameState.normal;
                    }

                    ShorteningTimer = 0;
                }
                
                ShorteningTimer += Time.fixedDeltaTime;
                break;
            case GameState.win:
                break;
            case GameState.lose:
                break;
            case GameState.pause:
                break;
            default:
                break;
        }

        
    }

    public void GenTrail(int NumLine) 
    {
        GameObject[] CubeLine = new GameObject[5];
        GameObject StartPoint = new GameObject("Line");
        StartPoint.transform.position = LineStartPosition;
        storage.storage[NumLine].LineHub = StartPoint.transform;
        for (int i = 0; i < CubeLine.Length; i++)
        {
            if (storage.storage[NumLine].blocks[i] > 0)// удаляeт блоки от 0 и менее.
            {
                CubeLine[i] = GameObject.Instantiate(CubePrefab, LineStartPosition, Quaternion.identity, StartPoint.transform);
                CubeLine[i].GetComponent<Cube>().InitPrefab(storage.storage[NumLine].blocks[i]);
                CubeLine[i].transform.Translate(CubeOffset * i, 0, 0);
            }
            
        }
        int FoodNums = storage.storage[NumLine].balls.Length;
        GameObject[] FoodLine = new GameObject[FoodNums];

        float FoodDistance = FloorWidth / (FoodNums + 1);
        for (int i = 0; i < FoodLine.Length; i++)
        {
            FoodLine[i] = GameObject.Instantiate(FoodPrefab, FoodStartPosition, Quaternion.identity, StartPoint.transform);
            FoodLine[i].GetComponent<Food>().InitPrefab(storage.storage[NumLine].balls[i]);
            FoodLine[i].transform.Translate(FoodDistance * (i + 1), 0, 0);
        }
    }
}
