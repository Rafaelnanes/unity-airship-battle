using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] GameObject HitEffect;
    [SerializeField] Transform Parent;
    [SerializeField] float Hp = 10f;
    [SerializeField] int EnemyHitScore = 5;
    [SerializeField] int EnemyKillScore = 50;
    private PlayerAction playerAction;

    private void Start()
    {
        playerAction = FindObjectOfType<PlayerAction>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Hp -= playerAction.GetPlayerDamage();
        if (Hp <= 0)
        {
            GameObject vfx = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
            vfx.transform.parent = Parent.transform;
            Destroy(gameObject);
            playerAction.AddPoints(EnemyKillScore);
        }
        else
        {
            Quaternion quaternion = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y + 180, Quaternion.identity.z - 0.45f);
            Vector3 vector3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject vfx = Instantiate(HitEffect, transform.position, quaternion);
            vfx.transform.parent = Parent.transform;
            playerAction.AddPoints(EnemyHitScore);
        }
    }

}
