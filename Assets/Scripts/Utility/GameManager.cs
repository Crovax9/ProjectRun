﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager
{
    private static GameManager _instance = null;

    private int distanceScore = 0;
    private int itemScore = 0;

    private const int cheeseScore = 10;
    private const int feverBonus = 20;

    private bool feverMode = false;

    public static GameManager Instance
    {
        get{
            if (_instance == null) _instance = new GameManager();
            return _instance;
        }
    }

    public void ScoreReset()
    {
        itemScore = 0;
        distanceScore = 0;
    }

    public bool FeverMode
    {
        get
        {
            return feverMode;
        }
        set
        {
            feverMode = value;
        }
    }

    
    public void CheeseScore()
    {
        if (FeverMode == false)
        {
            itemScore += cheeseScore;
            //UIManager.Instance.CheeseScoreBoard(cheeseScore);
        }
        else
        {
            itemScore += cheeseScore + feverBonus;
            //UIManager.Instance.CheeseScoreBoard(cheeseScore + feverBonus);
        }
    }
    
    public void Score(int distance)
    {
        distanceScore = distance;
    }

    public int Score()
    {
        return distanceScore + itemScore;
    }

    public void SceneMove(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
