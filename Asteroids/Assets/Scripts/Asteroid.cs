using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int hitpoints = 15;
    public ParticleSystem explosion;


    void OnCollisionEnter(Collision col)
    {
        ParticleSystem loadedSystem = LoadExplosionFromFile();

        if (col.gameObject.tag == "Bullet")
        {
            if (hitpoints <= 0)
            {
                Destroy(this.gameObject);
                Instantiate(loadedSystem, this.transform.position, Quaternion.identity);
            }
            else
            {
                hitpoints--;
                Debug.LogError(hitpoints);
            }
        }
    }

    private ParticleSystem LoadExplosionFromFile()
    {
        ParticleSystem loadedExplosion = Resources.Load<ParticleSystem>("Boom");
        if (loadedExplosion == null)
        {
            throw new FileNotFoundException("This file wasn't found");
        }
        return loadedExplosion;
    }

}
