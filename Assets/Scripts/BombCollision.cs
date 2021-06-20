using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollision : MonoBehaviour
{
    [SerializeField] GameObject BombExplosionEffect;
    [SerializeField] float sphereRadius = 4f;
    private Transform parent;

    private void Start()
    {
        parent = GameObject.FindGameObjectWithTag("SpawnRuntime").transform;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.name.Equals("Terrain"))
        {
            return;
        }
        GameObject vfx = Instantiate(BombExplosionEffect, other.gameObject.transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        Collider[] colliders = Physics.OverlapSphere(other.gameObject.transform.position, sphereRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.OnBombCollision();
            }
        }
    }



}
