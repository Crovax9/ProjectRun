using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public UILabel scoreBoard;
    public UISprite heart;


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
        ScoreBoard();
    }

    private void ScoreBoard()
    {
        scoreBoard.text = GameManager.Instance.Score().ToString();
    }

    public void HPMinus(float minus)
    {
        heart.fillAmount -= minus;
    }


}
