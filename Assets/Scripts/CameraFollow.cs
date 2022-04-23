using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target1;
    public Transform target2;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Awake() 
    {
        offset = transform.position - target1.position;
    }

    void FixedUpdate()
    {
        float desiredZ = (target1.position.z + target2.position.z) / 2 + offset.z;
        float smoothedZ = Mathf.Lerp(transform.position.z, desiredZ, smoothSpeed);

        transform.position = new Vector3(transform.position.x, transform.position.y, smoothedZ);
    }
}


