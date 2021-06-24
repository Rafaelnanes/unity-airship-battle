using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    private PlayerActions playerActions;
    private PlayerEffects playerEffects;
    private float playerDamage;
    private bool isDoubleDamage;
    private float doubleDamageTimeLimit;
    private float doubleDamageTriggered;

    private void Start()
    {
        playerActions = GetComponent<PlayerActions>();
        playerEffects = GetComponent<PlayerEffects>();
        doubleDamageTimeLimit = playerActions.GetDoubleDamageTimeLimit();
    }

    private void Update()
    {
        if (isDoubleDamage && Time.realtimeSinceStartup - doubleDamageTriggered > doubleDamageTimeLimit)
        {
            isDoubleDamage = false;
            playerEffects.OnDoubleDamageOff();
        }
    }

    public void OnDoubleDamage()
    {
        isDoubleDamage = true;
        doubleDamageTriggered = Time.realtimeSinceStartup;
        playerEffects.OnDoubleDamage();
    }

    public float GetDamage()
    {
        float value = playerDamage;
        if (isDoubleDamage)
        {
            value = value * 2;
        }
        return value;
    }

    public void SetDamage(float damage)
    {
        playerDamage = damage;
    }


}
