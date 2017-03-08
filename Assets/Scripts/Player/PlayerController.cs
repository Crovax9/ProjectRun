using UnityEngine;
using System.Collections;

public enum PlayerAnimation
{
    Idle = 0,
    Run = 1,
    Right = 2,
    Left = 3,
    Jump = 4,
    Slide = 5,
}

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;

    public Transform moveDummy;

    private AnimatorStateInfo characterAnimInfo;

    void Update()
    {
        playerAnim.SetInteger("Moving", 0);

        if (SwipeManager.Instance.isSwiping(SwipeDirection.Left))
        {
            AnimatorControll(PlayerAnimation.Left);
        }
        else if (SwipeManager.Instance.isSwiping(SwipeDirection.Right))
        {
            AnimatorControll(PlayerAnimation.Right);
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
        characterAnimInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
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
}
