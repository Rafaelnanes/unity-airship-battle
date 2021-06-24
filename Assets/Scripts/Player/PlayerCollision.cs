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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            OnGetItem(other);

        }
        else
        {
            OnGetHit();
        }
    }

    private void OnGetItem(Collider other)
    {
        Item.ItemType itemType = other.GetComponent<Item>().getItem();
        if (Item.ItemType.DOUBLE_DAMAGE.Equals(itemType))
        {
            playerAction.SetDoubleDamage();
        }

        if (Item.ItemType.RECOVERY.Equals(itemType))
        {
            playerAction.SetRecovery();
        }
        Destroy(other.gameObject);
    }

    private void OnGetHit()
    {
        if (name.Equals("Collider Left"))
        {
            playerAction.OnHit(PlayerHit.Location.LEFT);
        }

        if (name.Equals("Collider Right"))
        {
            playerAction.OnHit(PlayerHit.Location.RIGHT);
        }

        if (name.Equals("Collider Center"))
        {
            playerAction.OnHit(PlayerHit.Location.CENTER);
        }
    }
}
