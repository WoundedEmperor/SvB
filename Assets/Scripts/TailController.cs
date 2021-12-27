using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TailController : MonoBehaviour
{
    public Transform SnakeHead;
    public float DistanceBetweenSegments = 1f;
    public Transform TailPref;
    public Transform Snake;
    public Movement SnakeStats;
    public TextMeshPro ScoreText;

    private List<Transform> snakeCircles = new List<Transform>();
    public void SetLength(int NewLength)
    {
        ClearSnake();
        for (int i = 0; i < NewLength - 1; i++)
        {
            AddCircle(true);
        }
    }
    public void ClearSnake()
    {
        Transform FirstSegment = snakeCircles[0];
        if (snakeCircles.Count > 1)
        {
            Destroy(snakeCircles[1].gameObject);
        }        
        snakeCircles.Clear();
        snakeCircles.Add(FirstSegment);
        SnakeStats.Length = 1;
    }

    public void AddCircle(bool UpdateLength = false)
    {
        Transform PreviousSegment;
        if (snakeCircles.Count == 0)
        {
            PreviousSegment = SnakeHead;
        }
        else
        {
            PreviousSegment = snakeCircles[snakeCircles.Count - 1];
        }
                
        Vector3 position = new Vector3(PreviousSegment.position.x, SnakeHead.position.y, PreviousSegment.position.z - DistanceBetweenSegments);
        Transform NewSegment = Instantiate(TailPref, position, Quaternion.identity, Snake);
        NewSegment.GetComponent<Tail>().PreviousTail = PreviousSegment;
        snakeCircles.Add(NewSegment);

        if (UpdateLength)
        {
            SnakeStats.Length++;
            ScoreText.text = SnakeStats.Length.ToString();
        }
            
    }

    public void RemoveCircle()
    {
        if (snakeCircles.Count == 1)
        {
            SnakeStats.Length = 0;
            ScoreText.text = SnakeStats.Length.ToString();
            return;
        }
        Destroy(snakeCircles[snakeCircles.Count - 1].gameObject);
        snakeCircles.RemoveAt(snakeCircles.Count - 1);
        SnakeStats.Length--;
        ScoreText.text = SnakeStats.Length.ToString();
    }
}
