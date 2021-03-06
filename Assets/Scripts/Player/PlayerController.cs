﻿using UnityEngine;
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
    public CapsuleCollider playerCollider;

    private Transform moveDummy;
    private Transform player;

    private AnimatorStateInfo characterAnimInfo;
    

    private const float moveSpeed = 8.0f;
    private bool deathFlag = false;
    private bool noDamage = false;
    private float dummyMoveSpeed = 6.0f;


    void Awake()
    {
        moveDummy = GameObject.Find("MoveDummy").transform;
        player = GameObject.Find("Boar").transform;
    }

    void Update()
    {
        CharacterTranslate();
        CharacterControl();
    }

    void LateUpdate()
    {
        playerAnim.SetInteger("Moving", 0);
        GameManager.Instance.Score((int)PlayerDistance());

    }

    private float PlayerDistance()
    {
        return player.position.z;
    }


    private void CharacterTranslate()
    {
        if (!deathFlag)
        {
            moveDummy.Translate(0, 0, dummyMoveSpeed * Time.deltaTime);
            //playerAnim.SetInteger("Moving", 0);
            player.position = Vector3.MoveTowards(transform.position, moveDummy.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            GameManager.Instance.FeverMode = false;
            UIManager.Instance.tweenPosition.PlayForward();
        }
    }

    private void CharacterControl()
    {
        characterAnimInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
        if (SwipeManager.Instance.isSwiping(SwipeDirection.Up) || SwipeManager.Instance.isSwiping(SwipeDirection.LeftUp) || SwipeManager.Instance.isSwiping(SwipeDirection.RightUp))
        {
            AnimatorControll(PlayerAnimation.Jump);
        }
        else if (SwipeManager.Instance.isSwiping(SwipeDirection.Left))
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
                    StartCoroutine(JumpCollider());
                    break;

                default:

                    break;
            }
        }
        if (animation == PlayerAnimation.Hit)
        {
            playerAnim.SetInteger("Moving", 10);
        }
        else if (animation == PlayerAnimation.Slide)
        {
            playerAnim.SetInteger("Moving", 4);
        }
        else if (animation == PlayerAnimation.Death)
        {
            playerAnim.SetInteger("Moving", 12);
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

    private void CollisionCheckMethod(Collider col)
    {
        if (!noDamage && !deathFlag)
        {
            if (characterAnimInfo.IsTag("All"))
            {
                switch (col.name)
                {
                    case "Banana":
                        AnimatorControll(PlayerAnimation.Slide);
                        Debug.Log("Slide");
                        break;

                    case "Trap":
                        col.GetComponent<Animation>().Play("Take 001");
                        if (GameManager.Instance.FeverMode == false)
                        {
                            AnimatorControll(PlayerAnimation.Death);
                            UIManager.Instance.HPMinus(1.0f);
                            deathFlag = true;
                            Debug.Log("Death");
                        }
                        break;

                    default:
                        col.GetComponent<MeshSplit>().enabled = true;
                        if (GameManager.Instance.FeverMode == false)
                        {
                            UIManager.Instance.HPMinus(0.35f);
                            if (UIManager.Instance.heart.fillAmount == 0)
                            {
                                AnimatorControll(PlayerAnimation.Death);
                                deathFlag = true;
                            }
                            else
                            {
                                AnimatorControll(PlayerAnimation.Hit);
                                StartCoroutine(NoDamage(3.0f));
                            }
                        }
                        Debug.Log("Hit");
                        break;
                }
            }
            else if (characterAnimInfo.IsTag("Jump"))
            {
                if (col.name == "Rock")
                {
                    col.GetComponent<MeshSplit>().enabled = true;
                    if (GameManager.Instance.FeverMode == false)
                    {
                        if (UIManager.Instance.heart.fillAmount == 0)
                        {
                            AnimatorControll(PlayerAnimation.Death);
                            deathFlag = true;
                        }
                        else
                        {
                            AnimatorControll(PlayerAnimation.Hit);
                            StartCoroutine(NoDamage(3.0f));
                        }
                    }
                    Debug.Log("Hit");
                }
                else if (col.name == "Root")
                {
                    col.GetComponent<MeshSplit>().enabled = true;
                    if (GameManager.Instance.FeverMode == false)
                    {
                        if (UIManager.Instance.heart.fillAmount == 0)
                        {
                            AnimatorControll(PlayerAnimation.Death);
                            deathFlag = true;
                        }
                        else
                        {
                            AnimatorControll(PlayerAnimation.Hit);
                            StartCoroutine(NoDamage(3.0f));
                        }
                    }
                    Debug.Log("Hit");
                }
            }
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

    IEnumerator JumpCollider()
    {
        playerCollider.center = new Vector3(0.0f, 1.5f, 0.5f);
        yield return new WaitForSeconds(1.0f);
        playerCollider.center = new Vector3(0.0f, 0.5f, 0.5f);
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Maps"))
        {
            col.gameObject.SetActive(false);
            MapManager.Instance.MapSpawn(PlayerDistance());
        }
        if (col.CompareTag("Obstacles"))
        {
            CollisionCheckMethod(col);
        }
        if (col.CompareTag("Item"))
        {
            col.gameObject.SetActive(false);
            GameManager.Instance.CheeseScore();
            if (GameManager.Instance.FeverMode == false)
            {
                UIManager.Instance.CheeseSpritePlus();
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Obstacles"))
        {
            CollisionCheckMethod(col);
        }
        if (col.CompareTag("Item"))
        {
            col.gameObject.SetActive(false);
            GameManager.Instance.CheeseScore();
            if (GameManager.Instance.FeverMode == false)
            {
                UIManager.Instance.CheeseSpritePlus();
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Obstacles"))
        {
            CollisionCheckMethod(col);
        }
        if (col.CompareTag("Item"))
        {
            col.gameObject.SetActive(false);
            GameManager.Instance.CheeseScore();
            if (GameManager.Instance.FeverMode == false)
            {
                UIManager.Instance.CheeseSpritePlus();
            }
        }
    }
}
