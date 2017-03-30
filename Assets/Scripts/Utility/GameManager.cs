using UnityEngine;
using System.Collections;

public class GameManager
{
    private static GameManager _instance = null;

    private int score = 0;

    public static GameManager Instance
    {
        get{
            if (_instance == null) _instance = new GameManager();
            return _instance;
        }
    }

    public void Score(int distance, int cheese)
    {
        score = distance + cheese;
    }

    public int Score()
    {
        return score;
    }
}
