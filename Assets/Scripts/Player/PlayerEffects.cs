using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{

    [SerializeField] ParticleSystem RightSmoke;
    [SerializeField] ParticleSystem LeftSmoke;
    [SerializeField] ParticleSystem CenterSmoke;
    [SerializeField] ParticleSystem FireThrust;
    [SerializeField] ParticleSystem[] Fire;
    [SerializeField] GameObject Bomb;
    private Transform parent;
    private Transform playerHolder;

    private void Start()
    {
        parent = GameObject.FindGameObjectWithTag("SpawnRuntime").transform;
        playerHolder = GameObject.FindGameObjectWithTag("PlayerHolder").transform;
    }

    public void OnDamageRight()
    {
        RightSmoke.Play();
    }

    public void OnDamageLeft()
    {
        LeftSmoke.Play();
    }

    public void OnDamageCenter()
    {
        CenterSmoke.Play();
    }
    public void TurnOnEngine()
    {
        FireThrust.Play();
    }

    public void OnFire(bool isOnFire)
    {
        foreach (ParticleSystem localFire in Fire)
        {
            if (isOnFire)
            {
                localFire.Play();
            }
            else
            {
                localFire.Stop();
            }
        }

    }

    public void OnBomb()
    {
        GameObject vfx = Instantiate(Bomb, transform.position, playerHolder.rotation);
        vfx.transform.parent = parent.transform;
        vfx.GetComponent<ParticleSystem>().Emit(1);
    }
}
