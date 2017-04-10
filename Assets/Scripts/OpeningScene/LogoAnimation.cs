using UnityEngine;
using System.Collections;

public class LogoAnimation : MonoBehaviour
{
    [SerializeField]
    Animation openingAnim;

    void Awake()
    {
        openingAnim.Play("Appear");
        StartCoroutine(AnimPlay());  
    }

    IEnumerator AnimPlay()
    {
        while (true)
        {
            if (openingAnim.IsPlaying("Appear") == false)
            {
                openingAnim.Play("disappear");
                yield break;
            }
            yield return null;
           
        }
    }
}
