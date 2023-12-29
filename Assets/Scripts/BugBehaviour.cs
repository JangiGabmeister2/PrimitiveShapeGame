using UnityEngine;

public class BugBehaviour : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask walls;

    private bool touchingWall = false;

    private float switchSeconds = 3f;

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        touchingWall = Physics.Raycast(transform.position, transform.forward, 1f, walls);

        if (touchingWall)
        {
            transform.Rotate(new Vector3(-90, 0, 0));
        }
    }

    public void Squash()
    {
        Debug.Log("Bug squashed!");

        Destroy(gameObject);
    }
}
