using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public UILabel scoreBoard;

    private int score = 0;

    private static UIManager _instance = null;

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    void LateUpdate()
    {
        scoreBoard.text = score.ToString();
    }
        

    public void Score(int distance, int cheese)
    {
        score = distance + cheese;
        //return score;
    }
}
