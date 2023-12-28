using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform target;
    public Transform groundCheck;

    public Vector2 searchDistance = new Vector2(5f, 50f);
    public float moveSpeed = 8;

    private void Update()
    {
        MoveMonster();
    }

    private void MoveMonster()
    {
        if (IsGrounded() && TargetIsClose())
        {
            transform.LookAt(target);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    public bool IsGrounded()
    {
        bool isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, .5f);

        return isGrounded;
    }

    public bool TargetIsClose()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget > searchDistance.x && distanceToTarget <= searchDistance.y)
        {
            return true;
        }

        return false;
    }
}
