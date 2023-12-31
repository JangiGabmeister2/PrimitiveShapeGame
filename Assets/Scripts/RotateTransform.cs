using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTransform : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 5f;

    private void Update()
    {
        transform.Rotate(transform.up, _rotationSpeed * Time.deltaTime);
    }
}
