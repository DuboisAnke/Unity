using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnBounds : MonoBehaviour
{
    float x = 9.6f;
    float y = 5.6f;
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x >= x || this.transform.position.x <= -x
        || this.transform.position.y >= y || this.transform.position.y <= -y)
        {

            Destroy(this.gameObject);

        }
    }
}
