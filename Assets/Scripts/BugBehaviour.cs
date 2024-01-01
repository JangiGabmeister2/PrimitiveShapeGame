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

    Rigidbody _rb => GetComponent<Rigidbody>();

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
            transform.position += -transform.up * 2;
            transform.Rotate(Vector3.right * 90);

            moveSpeed = 2f;

            _rb.useGravity = true;
        }

        eatCake = Physics.Raycast(transform.position, transform.forward, 1f, cake);

        if (eatCake)
        {
            moveSpeed = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hammer"))
        {
            Squash();
        }
    }

    public void Squash()
    {
        Debug.Log("Bug Squashed!");

        Destroy(gameObject);
    }
}
