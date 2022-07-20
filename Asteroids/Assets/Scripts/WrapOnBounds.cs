using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapOnBounds : MonoBehaviour
{
    float x = 9.6f;
    float y = 5.6f;
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x >= x)
        {
           Vector3 originalPos = this.transform.position;
           Vector3 teleportPos = new Vector3(-x, originalPos.y, originalPos.z);

           this.transform.position = teleportPos;

        } else if (this.transform.position.x <= -x)
        {
            Vector3 originalPos = this.transform.position;
           Vector3 teleportPos = new Vector3(x, originalPos.y, originalPos.z);

           this.transform.position = teleportPos;
        }

        if (this.transform.position.y >= y)
        {
            Vector3 originalPos = this.transform.position;
            Vector3 teleportPos = new Vector3(originalPos.x, -y, originalPos.z);

            this.transform.position = teleportPos;
        } else if (this.transform.position.y <= -y)
        {
            Vector3 originalPos = this.transform.position;
            Vector3 teleportPos = new Vector3(originalPos.x, y, originalPos.z);

            this.transform.position = teleportPos;
        }
    }
}
