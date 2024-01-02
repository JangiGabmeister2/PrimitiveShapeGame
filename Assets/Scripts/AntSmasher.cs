using System.Collections;
using UnityEngine;

public class AntSmasher : MonoBehaviour
{
    public GameObject hammer, cake, cakeObj, aoeSphere;

    public LayerMask clickableLayer;

    public float hammerCooldown = 2f;
    private float _cooldown;

    private Vector3 aoe_originalPos;

    private void Start()
    {
        _cooldown = hammerCooldown;
        aoe_originalPos = aoeSphere.transform.position;
    }

    private void Update()
    {
        _cooldown -= Time.deltaTime;

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

        if (Input.GetMouseButtonUp(0) && _cooldown <= 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, clickableLayer))
            {
                StartCoroutine(CreateHammer(hitInfo.point + -Vector3.up));

                aoeSphere.transform.position = aoe_originalPos;
            }
        }
    }

    private IEnumerator CreateHammer(Vector3 position)
    {
        _cooldown = hammerCooldown;

        GameObject newHammer = Instantiate(hammer, position, Quaternion.identity, cake.transform);
        newHammer.transform.LookAt(cakeObj.transform.position);

        yield return new WaitForSeconds(3f);

        Destroy(newHammer);
    }
}
