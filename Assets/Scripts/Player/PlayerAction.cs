using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] float imunityTimeLimit = 3f;
    private Animator animator;
    private float imunityTimeTriggered;
    private PlayerEffects playerEffects;
    private PlayerMovement playerMovement;
    private bool isImune;

    private void Start()
    {
        playerEffects = GetComponent<PlayerEffects>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.realtimeSinceStartup > 4)
        {
            playerMovement.enabled = true;
        }
        if (isImune && Time.time - imunityTimeTriggered > imunityTimeLimit)
        {
            isImune = false;
        }

    }

    #region OnDamage
    public void OnDamageRight(float collisionUpperValue, float collisionCenterValue)
    {
        if (isImune)
        {
            return;
        }

        playerEffects.OnDamageRight();
        //playerMovement.Shaking(true);
        ShakeRight();
        SetImunity();
    }

    public void OnDamageLeft(float collisionUpperValue, float collisionCenterValue)
    {
        if (isImune)
        {
            return;
        }
        playerEffects.OnDamageLeft();
        //playerMovement.Shaking(true);
        ShakeLeft();
        isImune = true;
    }

    public void OnDamageCenter(float collisionUpperValue, float collisionCenterValue)
    {
        if (isImune)
        {
            return;
        }
        playerEffects.OnDamageCenter();
        //playerMovement.Shaking(true);
        float random = Random.Range(0f, 1f);
        if (random > 0.5)
        {
            ShakeRight();
        }
        else
        {
            ShakeLeft();
        }

        isImune = true;
    }

    private void ShakeRight()
    {
        //animator.SetTrigger("Shake Right");
    }

    private void ShakeLeft()
    {
        //animator.SetTrigger("Shake Left");
    }

    #endregion

    private void SetImunity()
    {
        isImune = true;
        imunityTimeTriggered = Time.time;
    }

}
