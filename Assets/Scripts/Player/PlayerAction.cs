using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] float imunityTimeLimit = 3f;
    private float imunityTimeTriggered;
    private PlayerEffects playerEffects;
    private PlayerMovement playerMovement;
    private bool isImune;

    private void Start()
    {
        playerEffects = FindObjectOfType<PlayerEffects>();
        playerMovement = FindObjectOfType<PlayerMovement>();
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
        Debug.Log($"Right Collision: {collisionUpperValue} / {collisionCenterValue}");
        if (isImune)
        {
            return;
        }

        playerEffects.OnDamageRight();
        playerMovement.Shake();
        SetImunity();
    }

    public void OnDamageLeft(float collisionUpperValue, float collisionCenterValue)
    {
        if (isImune)
        {
            return;
        }
        playerEffects.OnDamageLeft();
        playerMovement.Shake();
        isImune = true;
    }

    public void OnDamageCenter(float collisionUpperValue, float collisionCenterValue)
    {
        if (isImune)
        {
            return;
        }
        playerEffects.OnDamageCenter();
        playerMovement.Shake();
        isImune = true;
    }

    #endregion

    private void SetImunity()
    {
        isImune = true;
        imunityTimeTriggered = Time.time;
    }

}
