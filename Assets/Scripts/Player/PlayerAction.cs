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
            Debug.Log($"Imunity False");
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

    private void ShakeRight()
    {
        Debug.Log("Shake Right");
        animator.SetTrigger("Shake Right");
    }

    private void ShakeLeft()
    {
        Debug.Log("Shake Left");
        animator.SetTrigger("Shake Left");
    }

    #endregion

    private void SetImunity()
    {
        isImune = true;
        imunityTimeTriggered = Time.time;
        Debug.Log($"Imunity true");
    }

}
