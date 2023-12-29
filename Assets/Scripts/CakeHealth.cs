using System.Collections;
using UnityEngine;

public class CakeHealth : MonoBehaviour
{
    [Range(0f, 1f)] public float health = 1;

    public void DecreaseHealth()
    {
        health -= .01f;
    }

    public void DecreaseHealth(float decrement)
    {
        health -= decrement;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bug"))
        {
            StartCoroutine(nameof(LoseHealth));
        }
    }

    private IEnumerator LoseHealth()
    {
        DecreaseHealth();

        yield return new WaitForSeconds(1f);
    }
}
