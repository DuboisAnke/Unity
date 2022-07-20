using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Gun : MonoBehaviour
{
    GameObject loadedBullet;
    GameObject spawnedBullet;

    float time = 0;
    public float shootSpeed = 0.1f;
    List<GameObject> spawnedBullets = new List<GameObject>();

    void Start()
    {
        loadedBullet = LoadPrefabFromFile();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            time = 0;

        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (time > 0)
            {
                time -= Time.deltaTime;

            }
            else if (time <= 0f)
            {
                SpawnBullet();
                time = shootSpeed;
            }
        }


    }

    void SpawnBullet()
    {
        spawnedBullet = Instantiate(loadedBullet, this.transform.position, Quaternion.identity);
        spawnedBullets.Add(spawnedBullet);
        Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();

        float force = 7f;
        rb.AddForce(transform.up * force, ForceMode.VelocityChange);
    }

    private GameObject LoadPrefabFromFile()
    {
        GameObject loadedObject = Resources.Load<GameObject>("Bullet");
        if (loadedObject == null)
        {
            throw new FileNotFoundException("This file wasn't found");
        }
        return loadedObject;
    }
}
