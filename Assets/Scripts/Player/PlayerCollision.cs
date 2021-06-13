using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private PlayerEffects playerEffects;

    private void Start()
    {
        playerEffects = FindObjectOfType<PlayerEffects>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Name: {name} -> Collidiu em: {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Name: {name} -> Trigger: {other.gameObject.name}");
        if (name.Equals("Collider Left"))
        {
            playerEffects.OnDamageLeft();
        }

        if (name.Equals("Collider Right"))
        {
            playerEffects.OnDamageRight();
        }

        if (name.Equals("Collider Center"))
        {
            playerEffects.OnDamageCenter();
        }

    }
}
