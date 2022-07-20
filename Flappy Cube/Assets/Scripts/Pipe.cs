using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float speed;
    float leftEdge = -8.25f;

    void Update()
    {
        this.transform.position += Vector3.left * speed * Time.deltaTime;

        if (this.transform.position.x < leftEdge)
        {
            Destroy(this.gameObject);
        }
    }
}
