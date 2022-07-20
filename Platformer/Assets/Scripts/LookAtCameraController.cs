using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraController : MonoBehaviour
{
    public Transform target;
    Vector3 lookAtPosition;
    float lerpSpeed = 10f;

    public float smoothFactor = 0.5f;
    void Start()
    {
        lookAtPosition = target.position;

    }

    void LateUpdate()
    {

        lookAtPosition = Vector3.Lerp(lookAtPosition, target.position, Time.deltaTime * lerpSpeed);
        transform.LookAt(lookAtPosition);
    }
}
