using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AttackableObject : MonoBehaviour, IAttackable
{
    public float knockbackForce = 10f;
    private Rigidbody rb = null;

    void Awake()
    {
        this.rb = GetComponent<Rigidbody>();
    }


    public void GetAttacked(Transform attackerOrigin, Transform attackZoneOrigin)
    {
        Vector3 direction = this.transform.position - attackerOrigin.position;
        direction.Normalize();
        direction.y = 10f;

        // // You can add some upwards direction to this, and then renormalize, so it always gets knocked up a bit
        rb.AddForce(direction * knockbackForce, ForceMode.Impulse);

        StartCoroutine(RoutineExplode(0.25f));

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

    void Explode()
    {
        ParticleSystem loadedSystem = LoadExplosionFromFile();
        Instantiate(loadedSystem, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator RoutineExplode(float duration)
    {

        yield return new WaitForSeconds(duration);
        Explode();
    }
}
