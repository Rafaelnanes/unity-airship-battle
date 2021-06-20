using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] float InitialActionTime = 4f;
    [Header("Imunity")]
    [SerializeField] float ImunityTimeLimit = 3f;
    [Header("Damage")]
    [SerializeField] float PlayerAmmoDamage = 2f;
    [SerializeField] float PlayerBombDamage = 20f;
    [Header("Ammo")]
    [SerializeField] int BombCooldown = 5;
    [SerializeField] float AmmoCooldown = 3.5f;
    [SerializeField] float AmmoFactor = 0.05f;
    private PlayerEffects playerEffects;
    private PlayerMovement playerControl;
    private PlayerShoot playerShoot;
    private PlayerImunity playerImunity;
    private PlayerHit playerHit;
    private GameUI gameUI;
    private int playerScore;

    private void Start()
    {
        playerEffects = GetComponent<PlayerEffects>();
        playerControl = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
        playerHit = GetComponent<PlayerHit>();
        playerImunity = GetComponent<PlayerImunity>();
        gameUI = GetComponent<GameUI>();
    }

    private void Update()
    {
        EnableMovement();
        BeginThrustEffect();
        DisableImunity();
    }

    public void OnHit(PlayerHit.Location location)
    {
        playerHit.OnHit(location);
    }



    public void AddPoints(int value)
    {
        playerScore += value;
        gameUI.SetScore(playerScore);
    }

    #region Imunity
    private void DisableImunity()
    {
        playerImunity.Disable();
    }

    public float GetImunityTimeLimit()
    {
        return ImunityTimeLimit;
    }
    #endregion

    #region Shoot & Ammo
    public void OnShoot(float pressValue)
    {
        playerShoot.OnShoot(pressValue);
    }


    public void HasAmmo()
    {
        playerShoot.HasAmmo();
    }

    public void OutOfAmmo()
    {
        playerShoot.OutOfAmmo();
    }

    public float GetAmmoFactor()
    {
        return AmmoFactor;
    }

    public float GetRechargeTime()
    {
        return AmmoCooldown;
    }

    public float GetPlayerAmmoDamage()
    {
        return PlayerAmmoDamage;
    }

    public void OnBomb()
    {
        if (playerShoot.isBombEnabled())
        {
            playerShoot.OnBomb();
            gameUI.TriggerBombTimmer();
        }

    }


    public float GetPlayerBombDamage()
    {
        return PlayerBombDamage;
    }

    public int GetPlayerBombCooldown()
    {
        return BombCooldown;
    }

    public void EnableBomb()
    {
        playerShoot.EnableBomb();
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
        if (Time.realtimeSinceStartup > InitialActionTime)
        {
            playerControl.enabled = true;
            playerShoot.enabled = true;
        }
    }

}
