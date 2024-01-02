using System.Collections;
using UnityEngine;

public class BugBehaviour : MonoBehaviour
{
    public GameObject antObj;
    public ParticleSystem bloodSplatter;

    public float moveSpeed;
    public LayerMask walls;
    public LayerMask cake;

    private bool _touchingWall = false;
    private bool _seeCake = false;
    private bool _eatCake = false;

    [HideInInspector] public bool smashed = false;

    Rigidbody _rb => GetComponent<Rigidbody>();

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        _touchingWall = Physics.Raycast(transform.position, transform.forward, 1f, walls);

        if (_touchingWall)
        {
            transform.Rotate(Vector3.right * -90);
        }

        _seeCake = Physics.Raycast(transform.position, -transform.up, Mathf.Infinity, cake);

        if (_seeCake)
        {
            transform.position += -transform.up * 2;
            transform.Rotate(Vector3.right * 90);

            moveSpeed = 2f;

            _rb.useGravity = true;
        }

        _eatCake = Physics.Raycast(transform.position, transform.forward, 1f, cake);

        if (_eatCake)
        {
            moveSpeed = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hammer"))
        {
            smashed = true;

            StartCoroutine(Squash());
        }
    }

    public IEnumerator Squash()
    {
        ScoreManager.instance.AddScore(10);

        Destroy(antObj);

        bloodSplatter.Play();

        yield return new WaitForSeconds(10);

        Destroy(gameObject);
    }
}
