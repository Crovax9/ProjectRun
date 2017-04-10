using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class GameManager
{
    private static GameManager _instance = null;

    private int score = 0;

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
            score += cheeseScore;
            UIManager.Instance.CheeseScoreBoard(cheeseScore);
        }
        else
        {
            score += cheeseScore + feverBonus;
            UIManager.Instance.CheeseScoreBoard(cheeseScore + feverBonus);
        }
    }

    public void Score(int distance)
    {
        score = distance;
    }

    public int Score()
    {
        return score;
    }
}
