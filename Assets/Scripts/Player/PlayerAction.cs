using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] float ImunityTimeLimit = 3f;
    [SerializeField] float PlayerDamage = 2f;
    private Animator animator;
    private float imunityTimeTriggered;
    private PlayerEffects playerEffects;
    private PlayerMovement playerControl;
    private bool isImune;

    private void Start()
    {
        playerEffects = GetComponent<PlayerEffects>();
        playerControl = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        EnableMovement();
        BeginThrustEffect();
        DisableImunity();
    }

    private void BeginThrustEffect()
    {

        if (Time.realtimeSinceStartup > 4)
        {
            playerEffects.TurnOnEngine();
        }
    }

    private void DisableImunity()
    {
        if (isImune && Time.time - imunityTimeTriggered > ImunityTimeLimit)
        {
            isImune = false;
        }
    }

    private void EnableMovement()
    {
        if (Time.realtimeSinceStartup > 4)
        {
            playerControl.enabled = true;
        }
    }

    #region OnDamage
    public void OnDamageRight()
    {
        if (isImune)
        {
            return;
        }

        ShakeRight();
        playerEffects.OnDamageRight();
        SetImunity();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        playerEffects.OnFire(context.ReadValue<float>() > 0);
    }

    public void OnDamageLeft()
    {
        if (isImune)
        {
            return;
        }
        ShakeLeft();
        playerEffects.OnDamageLeft();
        SetImunity();
    }

    public void OnDamageCenter()
    {
        if (isImune)
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
        playerEffects.OnDamageCenter();

        SetImunity();
    }

    public float GetPlayerDamage()
    {
        return PlayerDamage;
    }

    private void ShakeRight()
    {
        animator.SetTrigger("Shake Right");
    }

    private void ShakeLeft()
    {
        animator.SetTrigger("Shake Left");
    }

    #endregion

    private void SetImunity()
    {
        isImune = true;
        imunityTimeTriggered = Time.time;
    }

}
