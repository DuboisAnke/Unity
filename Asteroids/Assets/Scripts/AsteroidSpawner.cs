using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    float x = 8.5f;
    float y = 4.5f;
    float time = 3f;
    
    GameObject loadedPrefab;
    GameObject spawnedPrefab;
    List<GameObject> spawnedAsteroids = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        loadedPrefab = LoadPrefabFromFile();
        Debug.LogError("De loaded prefab is: " + loadedPrefab);
        
        // without timer but invoke
        // InvokeRepeating(object, spawntime, spawndelay)

        // with timer
       
    }

    void Update() 
    {
        if (time > 0)
        {
            time  -= Time.deltaTime;

        } else if (time <= 0f)
        {
            SpawnAsteroid();
            time = 3f;
        }
    }

    void SpawnAsteroid () 
    {
        spawnedPrefab = Instantiate(loadedPrefab, new Vector3(Random.Range(-x,x), Random.Range(-y,y), 4), Quaternion.identity);
        Debug.LogError("SpawnAsteroid gets called");  
        spawnedAsteroids.Add(spawnedPrefab);
        Rigidbody rb = spawnedPrefab.GetComponent<Rigidbody>();

        float force = 40f;
        rb.AddForce(new Vector3(Random.Range(-force,force), Random.Range(-force,force), 0));

        
    }

    private GameObject LoadPrefabFromFile()
    {
        GameObject loadedObject = Resources.Load<GameObject>("Asteroid");
        if (loadedObject == null)
        {
            throw new FileNotFoundException("This file wasn't found");
        }
        return loadedObject;
    }
}
