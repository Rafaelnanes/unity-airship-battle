using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField] Text PlayerScoreText;
    [SerializeField] GameObject AmmoValue;
    [Header("Bomb")]
    [SerializeField] Text BombCooldownText;
    [SerializeField] GameObject BombPanel;
    private PlayerActions playerAction;
    private bool isOnFire;
    private float initialLocalScale;
    private float ammoFactor;
    private RectTransform ammoValue;
    private Image ammoImage;
    private float rechargeTime;
    private float bombCooldown;

    private void Start()
    {
        ammoValue = AmmoValue.GetComponent<RectTransform>();
        ammoImage = AmmoValue.GetComponent<Image>();
        initialLocalScale = ammoValue.localScale.x;
        playerAction = GetComponent<PlayerActions>();
        ammoFactor = playerAction.GetAmmoFactor();
        rechargeTime = playerAction.GetRechargeTime();
        BombPanel.SetActive(false);
    }

    void Update()
    {
        float ammoValue = CalculateAmmo();
        SetAmmoScale(ammoValue);
        SetAmmoColor(ammoValue);
        OutOfAmmo(ammoValue);
        BombCooldown();
    }

    private void BombCooldown()
    {
        if (bombCooldown > 0)
        {
            BombPanel.SetActive(true);
            bombCooldown -= Time.deltaTime;
            BombCooldownText.text = bombCooldown.ToString("0.0");
        }
        else
        {
            BombPanel.SetActive(false);
            playerAction.EnableBomb();
        }
    }

    private void OutOfAmmo(float ammoValue)
    {
        if (ammoValue == 0)
        {
            playerAction.OutOfAmmo();
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
                playerAction.OutOfAmmo();
            }
        }
        else
        {
            playerAction.HasAmmo();
            ammoImage.color = Color.green;
        }
    }

    public void SetScore(int value)
    {
        PlayerScoreText.text = value.ToString();
    }

    public void DecreaseAmmo()
    {
        isOnFire = true;
    }

    public void IncreaseAmmo()
    {
        isOnFire = false;
    }

    public void TriggerBombTimmer()
    {
        bombCooldown = playerAction.GetPlayerBombCooldown();
    }
}
