using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlappyController : MonoBehaviour
{

    public bool acceptInput = false;
    Rigidbody rb;
    public float flappyForce;
    public Action onOutOfBounds;
    public Action onCollideWithPipe;
    public Action onClearGap;
    GameObject lastTrigger;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    void Update()
    {
        float vertVelocity = rb.velocity.y;
        float percentage = Mathf.InverseLerp(-flappyForce, flappyForce, vertVelocity);

        float angle = Mathf.Lerp(-45, 45, percentage);
        this.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (acceptInput && Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector3(0, flappyForce, 0);
        }

        if (transform.position.y <= -3.54 || transform.position.y >= 5.50)
        {
            if (onOutOfBounds != null)
            {
                onOutOfBounds();
            }

        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Pipe")
        {
            if (onCollideWithPipe != null)
            {
                onCollideWithPipe();
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == lastTrigger)
        {
            return;
        }

        if (col.gameObject.tag == "Pipetrigger")
        {
            lastTrigger = col.gameObject;
            if (onClearGap != null)
            {
                onClearGap();
            }
        }
    }
}
