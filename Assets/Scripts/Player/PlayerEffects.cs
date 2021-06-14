using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{

    [SerializeField] GameObject RightSmoke;
    [SerializeField] GameObject LeftSmoke;
    [SerializeField] GameObject CenterSmoke;
    [SerializeField] GameObject FireThrust;
    [SerializeField] GameObject[] Fire;

    public void OnDamageRight()
    {
        RightSmoke.GetComponent<ParticleSystem>().Play();
    }

    public void OnDamageLeft()
    {
        LeftSmoke.GetComponent<ParticleSystem>().Play();
    }

    public void OnDamageCenter()
    {
        CenterSmoke.GetComponent<ParticleSystem>().Play();
    }
    public void TurnOnEngine()
    {
        FireThrust.GetComponent<ParticleSystem>().Play();
    }

    public void OnFire(bool isOnFire)
    {
        foreach (GameObject localFire in Fire)
        {
            if (isOnFire)
            {
                localFire.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                localFire.GetComponent<ParticleSystem>().Stop();
            }
        }

    }
}
