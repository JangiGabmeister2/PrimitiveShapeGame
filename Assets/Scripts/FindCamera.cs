using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCamera : MonoBehaviour
{
    [SerializeField] bool _faceCamera = false;

    Canvas canvas => GetComponent<Canvas>();
    Camera cam => Camera.main;

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        if (_faceCamera)
            canvas.transform.LookAt(
                transform.position + cam.transform.rotation * Vector3.back,
                cam.transform.rotation * Vector3.up);
    }
}
