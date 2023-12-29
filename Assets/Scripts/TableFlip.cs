using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableFlip : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bug"))
        {
            other.transform.rotation = Quaternion.AngleAxis(90, other.transform.right);
        }
    }
}
