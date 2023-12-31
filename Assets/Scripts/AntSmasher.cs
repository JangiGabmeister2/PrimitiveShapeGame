using System.Collections;
using UnityEngine;

public class AntSmasher : MonoBehaviour
{
    public GameObject hammer;
    public GameObject cake;
    public GameObject cakeObj;
    public GameObject aoeSphere;

    public LayerMask clickableLayer;

    public float hammerCooldown = 2f;
    private float cooldown;

    private Vector3 aoe_originalPos;

    private void Start()
    {
        cooldown = hammerCooldown;
        aoe_originalPos = aoeSphere.transform.position;
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, clickableLayer))
            {
                aoeSphere.transform.position = hitInfo.point;
            }
            else
            {
                aoeSphere.transform.position = aoe_originalPos;
            }
        }

        if (Input.GetMouseButtonUp(0) && cooldown <= 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, clickableLayer))
            {
                StartCoroutine(CreateHammer(hitInfo.point));

                aoeSphere.transform.position = aoe_originalPos;
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
