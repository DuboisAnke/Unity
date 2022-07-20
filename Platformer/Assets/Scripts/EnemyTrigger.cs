using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{

    void OnTriggerEnter(Collider col) 
    {
        ParticleSystem loadedSystem = LoadExplosionFromFile();
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            Instantiate(loadedSystem, col.transform.position, Quaternion.identity);
        }
    }

    private ParticleSystem LoadExplosionFromFile()
    {
        ParticleSystem loadedExplosion = Resources.Load<ParticleSystem>("Explosion");
        if (loadedExplosion == null)
        {
            throw new FileNotFoundException("This file wasn't found");
        }
        return loadedExplosion;
    }
}
