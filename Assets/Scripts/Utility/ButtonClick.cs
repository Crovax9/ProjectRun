using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour
{
    [SerializeField]
    private TweenPosition rankTween;
    [SerializeField]
    private TweenPosition optionTween;

    public void StartClick()
    {
        GameManager.Instance.SceneMove("Main");
    }

    public void MainButtonClick()
    {
        GameManager.Instance.SceneMove("StartScene");
    }

    public void RankButtonClick()
    {
        rankTween.PlayReverse();
        optionTween.PlayReverse();
    }

    public void OptionButtonClick()
    {
        rankTween.PlayForward();
        optionTween.PlayForward();
    }
}
