using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [Header("Imunity")]
    [SerializeField] float ImunityTimeLimit = 3f;
    [SerializeField] MeshRenderer Shield;
    [Header("Damage")]
    [SerializeField] float PlayerDamage = 2f;
    [Header("Ammo")]
    [SerializeField] float RechargeTime = 3.5f;
    [SerializeField] float AmmoFactor = 0.05f;
    private Animator animator;
    private float imunityTimeTriggered;
    private PlayerEffects playerEffects;
    private PlayerMovement playerControl;
    private GameUI gameUI;
    private bool isImune;
    private int playerScore;
    private bool isShootEnabled;

    private void Start()
    {
        playerEffects = GetComponent<PlayerEffects>();
        playerControl = GetComponent<PlayerMovement>();
        gameUI = GetComponent<GameUI>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        EnableMovement();
        BeginThrustEffect();
        DisableImunity();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!isShootEnabled)
        {
            return;
        }

        bool isOnFire = context.ReadValue<float>() > 0;
        if (isOnFire)
        {
            gameUI.DecreaseAmmo();
        }
        else
        {
            gameUI.IncreaseAmmo();
        }
        playerEffects.OnFire(isOnFire);
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

    public void AddPoints(int value)
    {
        playerScore += value;
        gameUI.SetScore(playerScore);
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

    #region Imunity
    private void DisableImunity()
    {
        if (isImune && Time.time - imunityTimeTriggered > ImunityTimeLimit)
        {
            isImune = false;
            Shield.enabled = false;
        }
    }

    private void SetImunity()
    {
        isImune = true;
        Shield.enabled = true;
        imunityTimeTriggered = Time.time;
    }
    #endregion

    #region Ammo
    public void SetFire(bool canShoot)
    {
        if (canShoot)
        {
            isShootEnabled = true;
        }
        else
        {
            isShootEnabled = false;
            playerEffects.OnFire(false);
        }
    }

    public float GetAmmoFactor()
    {
        return AmmoFactor;
    }

    public float GetRechargeTime()
    {
        return RechargeTime;
    }

    #endregion

    private void BeginThrustEffect()
    {
        if (Time.realtimeSinceStartup > 4)
        {
            playerEffects.TurnOnEngine();
        }
    }

    private void EnableMovement()
    {
        if (Time.realtimeSinceStartup > 4)
        {
            playerControl.enabled = true;
        }
    }

}
