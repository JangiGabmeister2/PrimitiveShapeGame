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

    private Vector3 clickPos = Vector3.zero;

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
                clickPos = hitInfo.point;
            }

            StartCoroutine(nameof(CreateHammer));

            cooldown = hammerCooldown;
        }
    }

    private IEnumerator CreateHammer()
    {
        GameObject newHammer = Instantiate(hammer, clickPos, Quaternion.identity, cake.transform);
        newHammer.transform.LookAt(cakeObj.transform.position);

        yield return new WaitForSeconds(3f);

        Destroy(newHammer);
    }
}
