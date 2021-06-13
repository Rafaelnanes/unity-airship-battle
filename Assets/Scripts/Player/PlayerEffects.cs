using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{

    [SerializeField] GameObject rightSmoke;
    [SerializeField] GameObject leftSmoke;
    [SerializeField] GameObject centerSmoke;
    [SerializeField] GameObject fireThrust;
    [SerializeField] GameObject[] fire;

    public void OnDamageRight()
    {
        rightSmoke.GetComponent<ParticleSystem>().Play();
    }

    public void OnDamageLeft()
    {
        leftSmoke.GetComponent<ParticleSystem>().Play();
    }

    public void OnDamageCenter()
    {
        centerSmoke.GetComponent<ParticleSystem>().Play();
    }
    public void TurnOnEngine()
    {
        fireThrust.GetComponent<ParticleSystem>().Play();
    }

    public void OnFire(bool isOnFire)
    {
        foreach (GameObject localFire in fire)
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
