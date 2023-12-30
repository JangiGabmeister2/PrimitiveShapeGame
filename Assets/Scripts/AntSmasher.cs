using System.Collections;
using UnityEngine;

public class AntSmasher : MonoBehaviour
{
    public GameObject hammer;
    public GameObject cake;
    public GameObject cakeObj;

    public LayerMask clickableLayer;

    public float hammerCooldown = 2f;
    private float cooldown;

    private void Start()
    {
        cooldown = hammerCooldown;
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cooldown <= 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, clickableLayer))
            {
                StartCoroutine(CreateHammer(hitInfo.point));
            }
        }
    }

    private IEnumerator CreateHammer(Vector3 position)
    {
        cooldown = hammerCooldown;

        GameObject newHammer = Instantiate(hammer, position, Quaternion.identity, cake.transform);
        newHammer.transform.LookAt(cakeObj.transform.position);

        yield return new WaitForSeconds(3f);

        Destroy(newHammer);
    }
}
