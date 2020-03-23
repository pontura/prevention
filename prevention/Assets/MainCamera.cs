using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Vector3 lookAtOffset;
    [SerializeField] private Vector3 offsetPos;
    [SerializeField] private Transform target;

    public float smoothTraslate = 0.05f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 _lookAtOffset = Vector3.Lerp(transform.position, target.transform.position + lookAtOffset, smoothTraslate);        
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offsetPos, smoothTraslate);
        transform.LookAt(_lookAtOffset);
    }
}
