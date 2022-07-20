using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcesController : MonoBehaviour
{
    public float force = 5f;
    public float speed;
    public float angularSpeed;

    public ParticleSystem particles;
    
    Rigidbody rb;
    
    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody>();
        if (rb)
        {
            Debug.LogError("Rigidbody is found");
        }
        
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        rb.AddForce(vertical * force * transform.up);
        if (vertical < 0 || vertical > 0)
        {
            particles.Play();
        } else if (vertical == 0)
        {
            particles.Stop();
        }

        // deals with the rotation 
        float horizontal = Input.GetAxis("Horizontal");

        float rotation = -horizontal * angularSpeed;

        rb.angularVelocity = new Vector3(0,0,rotation*Mathf.Deg2Rad);

        
    }
}
