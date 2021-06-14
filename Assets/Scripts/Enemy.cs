using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] Transform Parent;
    private void OnParticleCollision(GameObject other)
    {
        GameObject vfx = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        vfx.transform.parent = Parent.transform;
        Destroy(gameObject);
    }

}
