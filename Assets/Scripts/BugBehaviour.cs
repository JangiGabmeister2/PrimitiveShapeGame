using System.Collections;
using UnityEngine;

public class BugBehaviour : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask walls;
    public LayerMask cake;

    private bool touchingWall = false;
    private bool seeCake = false;
    private bool eatCake = false;

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        touchingWall = Physics.Raycast(transform.position, transform.forward, 1f, walls);

        if (touchingWall)
        {
            transform.Rotate(Vector3.right * -90);
        }

        seeCake = Physics.Raycast(transform.position, -transform.up, Mathf.Infinity, cake);

        if (seeCake)
        {
            transform.Rotate(Vector3.right * 90);

            moveSpeed = 2f;
        }

        eatCake = Physics.Raycast(transform.position, transform.forward, 1f, cake);

        if (eatCake)
        {
            moveSpeed = 0;
        }
    }

    public void Squash()
    {
        Debug.Log("Bug squashed!");

        Destroy(gameObject);
    }
}
