using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [Header("Imunity")]
    [SerializeField] float ImunityTimeLimit = 3f;
    [Header("Damage")]
    [SerializeField] float PlayerDamage = 2f;
    [Header("Ammo")]
    [SerializeField] float AmmoRechargeTime = 3.5f;
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

    public float GetPlayerDamage()
    {
        return PlayerDamage;
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
        return AmmoRechargeTime;
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
