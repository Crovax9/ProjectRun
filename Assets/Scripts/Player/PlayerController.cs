using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerAnimation
{
    Idle = 0,
    Run = 1,
    Right = 2,
    Left = 3,
    Jump = 4,
    Slide = 5,
    Hit = 6,
    Slip = 7,
    Death = 8,



    None = 20,

}

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;
    public List<SkinnedMeshRenderer> meshRenderer;

    private Transform moveDummy;
    private Transform player;

    private AnimatorStateInfo characterAnimInfo;
    

    private const float moveSpeed = 7.0f;
    private bool deathFlag = false;
    private bool noDamage = false;
    private float dummyMoveSpeed = 5.0f;

    void Start()
    {
        moveDummy = GameObject.Find("MoveDummy").transform;
        player = GameObject.Find("Boar").transform;
    }

    void FixedUpdate()
    {
        if (!deathFlag)
        {
            moveDummy.Translate(0, 0, dummyMoveSpeed * Time.deltaTime);
            playerAnim.SetInteger("Moving", 0);
            player.position = Vector3.MoveTowards(transform.position, moveDummy.position, moveSpeed * Time.deltaTime);
        }
    }

    void Update()
    {
        characterAnimInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
        if (SwipeManager.Instance.isSwiping(SwipeDirection.Left))
        {
            if (player.position.x > -2.0f)
            {
                AnimatorControll(PlayerAnimation.Left);
            }
        }
        else if (SwipeManager.Instance.isSwiping(SwipeDirection.Right))
        {
            if (player.position.x < 2.0f)
            {
                AnimatorControll(PlayerAnimation.Right);
            }
        }
        else if (SwipeManager.Instance.isSwiping(SwipeDirection.Up))
        {
            AnimatorControll(PlayerAnimation.Jump);
        }
        else if (SwipeManager.Instance.isSwiping(SwipeDirection.Down))
        {
            AnimatorControll(PlayerAnimation.Slide);
        }
    }

    private void AnimatorControll(PlayerAnimation animation)
    {
        if (characterAnimInfo.IsName("Running"))
        {
            switch (animation)
            {
                case PlayerAnimation.Left:
                    playerAnim.SetInteger("Moving", 1);
                    DummyMove(animation);
                    break;

                case PlayerAnimation.Right:
                    playerAnim.SetInteger("Moving", 2);
                    DummyMove(animation);
                    break;

                case PlayerAnimation.Jump:
                    playerAnim.SetInteger("Moving", 3);
                    break;

                case PlayerAnimation.Slide:
                    playerAnim.SetInteger("Moving", 4);
                    break;

                default:

                    break;
            }
        }
        if (animation == PlayerAnimation.Hit)
        {
            playerAnim.SetInteger("Moving", 10);
            Debug.Log("call2");
        }
        else if (animation == PlayerAnimation.Slip)
        {
            playerAnim.SetInteger("Moving", 11);
            Debug.Log("call3");
        }
        else if (animation == PlayerAnimation.Death)
        {
            playerAnim.SetInteger("Moving", 12);
        }
    }

    private float PlayerDistance()
    {
        return player.position.z;
    }

    private void DummyMove(PlayerAnimation direction)
    {
        if (direction == PlayerAnimation.Right)
        {
            moveDummy.Translate(Vector3.right);
        }
        else if (direction == PlayerAnimation.Left)
        {
            moveDummy.Translate(Vector3.left);
        }
    }


    IEnumerator NoDamage(float waitTime)
    {
        noDamage = true;
        var endTime = Time.time + waitTime;
        while (Time.time < endTime)
        {
            meshRenderer.ForEach(render => render.enabled = false);
            yield return new WaitForSeconds(0.1f);
            meshRenderer.ForEach(render => render.enabled = true);
            yield return new WaitForSeconds(0.1f);
        }
        noDamage = false;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Maps"))
        {
            col.gameObject.SetActive(false);
            MapManager.Instance.MapSpawn(PlayerDistance());
        }
        if (!noDamage && !deathFlag)
        {
            if (col.CompareTag("Obstacles"))
            {
                if (MapManager.Instance.ObstaclesCollisionCheck(col.gameObject, characterAnimInfo) == PlayerAnimation.Hit.ToString())
                {
                    AnimatorControll(PlayerAnimation.Hit);
                    StartCoroutine(NoDamage(3.0f));
                    Debug.Log("call");
                }
                else if (MapManager.Instance.ObstaclesCollisionCheck(col.gameObject, characterAnimInfo) == PlayerAnimation.Slide.ToString())
                {
                    AnimatorControll(PlayerAnimation.Slide);
                }
                else if (MapManager.Instance.ObstaclesCollisionCheck(col.gameObject, characterAnimInfo) == PlayerAnimation.Death.ToString())
                {
                    AnimatorControll(PlayerAnimation.Death);
                    col.GetComponent<Animation>().Play("Take 001");
                    deathFlag = true;
                }
            }
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (!noDamage && !deathFlag)
        {
            if (col.CompareTag("Obstacles"))
            {
                if (MapManager.Instance.ObstaclesCollisionCheck(col.gameObject, characterAnimInfo) == PlayerAnimation.Hit.ToString())
                {
                    AnimatorControll(PlayerAnimation.Hit);
                    StartCoroutine(NoDamage(3.0f));
                    Debug.Log("call");
                }
                else if (MapManager.Instance.ObstaclesCollisionCheck(col.gameObject, characterAnimInfo) == PlayerAnimation.Slide.ToString())
                {
                    AnimatorControll(PlayerAnimation.Slide);
                }
                else if (MapManager.Instance.ObstaclesCollisionCheck(col.gameObject, characterAnimInfo) == PlayerAnimation.Death.ToString())
                {
                    AnimatorControll(PlayerAnimation.Death);
                    col.GetComponent<Animation>().Play("Take 001");
                    deathFlag = true;
                }
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (!noDamage && !deathFlag)
        {
            if (col.CompareTag("Obstacles"))
            {
                if (MapManager.Instance.ObstaclesCollisionCheck(col.gameObject, characterAnimInfo) == PlayerAnimation.Hit.ToString())
                {
                    AnimatorControll(PlayerAnimation.Hit);
                    StartCoroutine(NoDamage(3.0f));
                    Debug.Log("call");
                }
                else if (MapManager.Instance.ObstaclesCollisionCheck(col.gameObject, characterAnimInfo) == PlayerAnimation.Slide.ToString())
                {
                    AnimatorControll(PlayerAnimation.Slide);
                }
                else if (MapManager.Instance.ObstaclesCollisionCheck(col.gameObject, characterAnimInfo) == PlayerAnimation.Death.ToString())
                {
                    AnimatorControll(PlayerAnimation.Death);
                    col.GetComponent<Animation>().Play("Take 001");
                    deathFlag = true;
                }
            }
        }
    }


}
