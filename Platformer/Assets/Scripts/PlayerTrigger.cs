using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public GameObject endScreen;

    void Start() 
    {
        endScreen.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.tag == "Player")
        {

            endScreen.gameObject.SetActive(true);
        }
    }
}
