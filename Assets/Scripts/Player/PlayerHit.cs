using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private Animator animator;
    private PlayerImunity playerImunity;
    private PlayerEffects playerEffects;
    public enum Location { LEFT, RIGHT, CENTER };

    void Start()
    {
        animator = GetComponent<Animator>();
        playerImunity = GetComponent<PlayerImunity>();
        playerEffects = GetComponent<PlayerEffects>();
    }

    public void OnHit(Location location)
    {
        if (location.Equals(Location.LEFT))
        {
            HitLeft();
        }
        else if (location.Equals(Location.RIGHT))
        {
            HitRight();
        }
        else
        {
            HitCenter();
        }
    }

    public void OnRecovery()
    {
        playerEffects.ResetSmokes();
    }

    private void HitRight()
    {
        if (playerImunity.isImune())
        {
            return;
        }

        ShakeRight();
        playerEffects.OnDamage(PlayerHit.Location.RIGHT);
        playerImunity.Enable();
    }

    private void HitLeft()
    {
        if (playerImunity.isImune())
        {
            return;
        }
        ShakeLeft();
        playerEffects.OnDamage(PlayerHit.Location.LEFT);
        playerImunity.Enable();
    }

    private void HitCenter()
    {
        if (playerImunity.isImune())
        {
            return;
        }
        float random = Random.Range(0f, 1f);
        if (random > 0.5)
        {
            ShakeRight();
        }
        else
        {
            ShakeLeft();
        }
        playerEffects.OnDamage(PlayerHit.Location.CENTER);
        playerImunity.Enable();
    }

    private void ShakeRight()
    {
        animator.SetTrigger("Shake Right");
    }

    private void ShakeLeft()
    {
        animator.SetTrigger("Shake Left");
    }

}
