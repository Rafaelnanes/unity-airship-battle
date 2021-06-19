using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private GameUI gameUI;
    private bool canShoot;
    private PlayerEffects playerEffects;
    private void Start()
    {
        gameUI = GetComponent<GameUI>();
        playerEffects = GetComponent<PlayerEffects>();
    }

    public void OnShoot(float pressValue)
    {
        if (!canShoot)
        {
            return;
        }

        bool isOnFire = pressValue > 0;
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

    public bool isShootEnabled()
    {
        return canShoot;
    }

    public void HasAmmo()
    {
        canShoot = true;
    }

    public void OutOfAmmo()
    {
        canShoot = false;
        playerEffects.OnFire(false);
    }
}
