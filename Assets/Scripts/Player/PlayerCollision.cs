using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerActions playerAction;

    private void Start()
    {
        playerAction = FindObjectOfType<PlayerActions>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Name: {name} -> Collidiu em: {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (name.Equals("Collider Left"))
        {
            playerAction.OnDamageLeft();
        }

        if (name.Equals("Collider Right"))
        {
            playerAction.OnDamageRight();
        }

        if (name.Equals("Collider Center"))
        {
            playerAction.OnDamageCenter();
        }
    }
}
