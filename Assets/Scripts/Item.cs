using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        DOUBLE_DAMAGE,
        RECOVERY
    }

    [SerializeField] ItemType item;


    public ItemType getItem()
    {
        return this.item;
    }

}
