using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    //Parameters
    [SerializeField] Text scoreText;
    
    //Cashe
    int totalScore;
    
    private void Start()
    {
        totalScore = 0;
        scoreText.text = totalScore.ToString();
    }

    public void AddScore(int pointValue)
    {
        totalScore += pointValue;
        scoreText.text = totalScore.ToString();
    }
}
