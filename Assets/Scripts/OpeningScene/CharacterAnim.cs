using UnityEngine;
using System.Collections;

public class CharacterAnim : MonoBehaviour 
{
    [SerializeField]
    GameObject target;
    [SerializeField]
    Animator animator;
    [SerializeField]
    TweenAlpha tween;

    void Start()
    {
        StartCoroutine(MoveCharacter());
    }

    IEnumerator MoveCharacter()
    {
        while (true)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, 7.0f * Time.deltaTime);
            yield return null;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Finish"))
        {
            animator.SetInteger("Moving", 9);
            tween.enabled = true;
        }
    }
}
