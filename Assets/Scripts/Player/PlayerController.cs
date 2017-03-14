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
    Death = 7,
}

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;
    public List<SkinnedMeshRenderer> renderer;

    private Transform moveDummy;
    private Transform player;

    private AnimatorStateInfo characterAnimInfo;
    

    private const float moveSpeed = 6f;
    private bool deathFlag = false;

    void Start()
    {
        moveDummy = GameObject.Find("MoveDummy").transform;
        player = GameObject.Find("Boar").transform;
    }

    void FixedUpdate()
    {
        if (!deathFlag)
        {
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
        else if (animation == PlayerAnimation.Death)
        {
            playerAnim.SetInteger("Moving", 11);
        }
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
        renderer.ForEach(render => render.enabled = false);
        yield return new WaitForSeconds(0.2f);
        renderer.ForEach(render => render.enabled = true);
        yield return new WaitForSeconds(0.2f);

    }


    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Maps"))
        {
            col.gameObject.SetActive(false);
            MapManager.Instance.Level1MapSetting();
        }
        else if (col.CompareTag("Obstacles"))
        {
            Debug.Log("call3");
            if (MapManager.Instance.ObstaclesCollisionCheck(col.gameObject, characterAnimInfo))
            {
                AnimatorControll(PlayerAnimation.Hit);
                StartCoroutine(NoDamage(2.0f));
                Debug.Log("call");
            }
            //Obstacles Judgement
            //Player Animation Method
            //Obstacles Destroy Method
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Obstacles"))
        {
            Debug.Log("call3");
            if (MapManager.Instance.ObstaclesCollisionCheck(col.gameObject, characterAnimInfo))
            {
                AnimatorControll(PlayerAnimation.Hit);
                Debug.Log("call");
            }
            //Obstacles Judgement
            //Player Animation Method
            //Obstacles Destroy Method
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Obstacles"))
        {
            Debug.Log("call3");
            if (MapManager.Instance.ObstaclesCollisionCheck(col.gameObject, characterAnimInfo))
            {
                AnimatorControll(PlayerAnimation.Hit);
                Debug.Log("call");
            }
            //Obstacles Judgement
            //Player Animation Method
            //Obstacles Destroy Method
        }
    }


}
