using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    public Animator monstAnimator;
    public MonsterMovement mover;

    private void Update()
    {
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        bool isGrounded = mover.IsGrounded();
        bool isMoving = mover.TargetIsClose();

        monstAnimator.SetBool("IsGrounded", isGrounded);
        monstAnimator.SetBool("IsMoving", isMoving);
    }
}
