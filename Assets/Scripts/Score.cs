using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshPro LevelScoreText;
    public Movement SnakeLengthScore; 
    
    public void InitPrefab(int scoreNum)
    {
        //= scoreNum;
        LevelScoreText.text = "Score: " + SnakeLengthScore.ToString();

    }
}
