using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UIManager : MonoBehaviour
{
    public UILabel scoreBoard;
    public UISprite heart;
    public UISprite cheeseSprite;
    public GameObject FeverLabel;

    public TweenPosition tweenPosition;

    public List<GameObject> cheeseScoreBoard;

    private static UIManager _instance = null;

    private Vector3 cheeseScoreBoardPosition = new Vector3(35.0f, 10.0f, 0f);


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
        ScoreBoardReset();
        ScoreBoard();
    }

    /*
    public void CheeseScoreBoard(int score)
    {
        var Board = cheeseScoreBoard.Where(board => board.activeInHierarchy == false).First();
        Board.GetComponent<UILabel>().text = "+" + score.ToString();
        Board.SetActive(true);
        Board.GetComponent<TweenPosition>().enabled = true;
        StartCoroutine(BoardReset(Board));
    }
    
    IEnumerator BoardReset(GameObject board)
    {
        yield return new WaitForSeconds(2.0f);
        board.SetActive(false);
        board.transform.localPosition = cheeseScoreBoardPosition;
    }
    */


    private void ScoreBoardReset()
    {
        scoreBoard.text = "0";
    }

    private void ScoreBoard()
    {
        StartCoroutine(ScoreBoardRoutine());
    }

    IEnumerator ScoreBoardRoutine()
    {
        while (true)
        {
            scoreBoard.text = GameManager.Instance.Score().ToString();
            yield return new WaitForSeconds(2.0f);
        }
    }

    public void HPMinus(float minus)
    {
        heart.fillAmount -= minus;
    }

    public void CheeseSpritePlus()
    {        
        cheeseSprite.fillAmount += 0.01f;
        if (cheeseSprite.fillAmount == 1.0f)
        {
            StartCoroutine(feverCoroutine());
        }
    }

    IEnumerator feverCoroutine()
    {
        FeverLabel.SetActive(true);
        GameManager.Instance.FeverMode = true;
        yield return new WaitForSeconds(10.0f);
        GameManager.Instance.FeverMode = false;
        cheeseSprite.fillAmount = 0;
        FeverLabel.SetActive(false);
    }
}
