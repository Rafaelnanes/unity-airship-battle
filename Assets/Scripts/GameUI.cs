using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Text PlayerScore;
    [SerializeField] GameObject AmmoValue;
    private PlayerAction playerAction;
    private bool isOnFire;
    private float initialLocalScale;
    private float ammoFactor;
    private RectTransform ammoValue;
    private Image ammoImage;
    private float rechargeTime;

    private void Start()
    {
        ammoValue = AmmoValue.GetComponent<RectTransform>();
        ammoImage = AmmoValue.GetComponent<Image>();
        initialLocalScale = ammoValue.localScale.x;
        playerAction = GetComponent<PlayerAction>();
        ammoFactor = playerAction.GetAmmoFactor();
        rechargeTime = playerAction.GetRechargeTime();
    }

    void Update()
    {
        float ammoValue = CalculateAmmo();
        SetAmmoScale(ammoValue);
        SetAmmoColor(ammoValue);
        OutOfAmmo(ammoValue);
    }

    private void OutOfAmmo(float ammoValue)
    {
        if (ammoValue == 0)
        {
            playerAction.SetFire(false);
            isOnFire = false;
        }
    }

    private float CalculateAmmo()
    {
        float offset = ammoFactor * Time.deltaTime;
        if (isOnFire)
        {
            offset = -offset;
        }
        return Mathf.Clamp(ammoValue.localScale.x + offset, 0, initialLocalScale);
    }

    private void SetAmmoScale(float xScale)
    {
        ammoValue.localScale = new Vector3(xScale, ammoValue.localScale.y, ammoValue.localScale.z);
    }

    private void SetAmmoColor(float xScale)
    {
        float value = xScale / initialLocalScale;
        if (value < rechargeTime / 10)
        {
            ammoImage.color = Color.red;
            if (!isOnFire)
            {
                playerAction.SetFire(false);
            }
        }
        else
        {
            playerAction.SetFire(true);
            ammoImage.color = Color.green;
        }
    }

    public void SetScore(int value)
    {
        PlayerScore.text = value.ToString();
    }

    public void DecreaseAmmo()
    {
        isOnFire = true;
    }

    public void IncreaseAmmo()
    {
        isOnFire = false;
    }
}
