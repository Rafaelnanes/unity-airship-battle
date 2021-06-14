using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    [SerializeField] float DestroyTime = 3f;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }


}
