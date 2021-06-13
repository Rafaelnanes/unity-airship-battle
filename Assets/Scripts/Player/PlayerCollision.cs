using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] float threshold = 0.3f;
    private PlayerAction playerAction;

    private void Start()
    {
        playerAction = FindObjectOfType<PlayerAction>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Name: {name} -> Collidiu em: {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log($"Name: {name} -> Collidiu em: {other.gameObject.name} -> {other.gameObject.transform.position - transform.position}");
        float collisionUpperValue = (other.gameObject.transform.position - transform.position).y;
        float collisionCenterValue = (other.gameObject.transform.position - transform.position).z;

        if (name.Equals("Collider Left"))
        {
            playerAction.OnDamageLeft(collisionUpperValue, collisionCenterValue);
        }

        if (name.Equals("Collider Right"))
        {
            playerAction.OnDamageRight(collisionUpperValue, collisionCenterValue);
        }

        if (name.Equals("Collider Center"))
        {
            playerAction.OnDamageCenter(collisionUpperValue, collisionCenterValue);
        }
    }
}
