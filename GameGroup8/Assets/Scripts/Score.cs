﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {


    public static int score;
    public Text scoreText;

    // Might fix thread-colliding
    /* void LateUpdate()
    {
        scoreText.text = "Score: " + score;
    }
    */

    public int getScore()
    {
        return score;
    }

   

    public void addScoreWave(int CurrentWave)
    {
        score += ((CurrentWave - 0) * 1000);
        scoreText.text = "Score: " + score;
    }

    public void addScoreEnemy(int EnemyLevel)
    {
        score += 500 * EnemyLevel;
        scoreText.text = "Score: " + score;
    }
    
    public void addScoreBuilding(int cost)
    {
        score += 500 * cost;
        scoreText.text = "Score: " + score;
    }

    
}