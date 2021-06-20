using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] GameObject HitEffect;
    [Header("Bomb")]
    [SerializeField] GameObject BombExplosionEffect;
    [SerializeField] float Hp = 10f;
    [SerializeField] int EnemyHitScore = 5;
    [SerializeField] int EnemyKillScore = 50;
    private PlayerActions playerAction;
    private Transform parent;

    private void Start()
    {
        playerAction = FindObjectOfType<PlayerActions>();
        parent = GameObject.FindGameObjectWithTag("SpawnRuntime").transform;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Ammo"))
        {
            Hp -= playerAction.GetPlayerAmmoDamage();
            if (Hp <= 0)
            {
                GameObject vfx = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
                vfx.transform.parent = parent.transform;
                Destroy(gameObject);
                playerAction.AddPoints(EnemyKillScore);
            }
            else
            {
                Quaternion quaternion = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y + 180, Quaternion.identity.z - 0.45f);
                GameObject vfx = Instantiate(HitEffect, transform.position, quaternion);
                vfx.transform.parent = parent.transform;
                playerAction.AddPoints(EnemyHitScore);
            }
        }

    }

    public void OnBombCollision()
    {
        Hp -= playerAction.GetPlayerBombDamage();
        if (Hp <= 0)
        {
            GameObject vfx = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
            vfx.transform.parent = parent.transform;
            Destroy(gameObject);
            playerAction.AddPoints(EnemyKillScore);
        }
        else
        {
            playerAction.AddPoints(EnemyHitScore);
        }
    }

}
