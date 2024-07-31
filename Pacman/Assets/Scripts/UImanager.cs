using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager sharedInstance;
    public Text scoreLabel;
    private int totalScore;

    private void Awake()
    {
        sharedInstance = this;
        totalScore = 0;
    }

    private void Update()
    {
        
    }

    public void ScorePoints(int points)
    {
        totalScore += points;
        scoreLabel.text = "Score: " + totalScore;
    }
}
