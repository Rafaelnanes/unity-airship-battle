using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

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
    private Dictionary<ParticleSystem, MinMaxGradient> shootDictionary;

    private void Start()
    {
        parent = GameObject.FindGameObjectWithTag("SpawnRuntime").transform;
        playerHolder = GameObject.FindGameObjectWithTag("PlayerHolder").transform;
        shootDictionary = new Dictionary<ParticleSystem, MinMaxGradient>();
        foreach (ParticleSystem localFire in Fire)
        {
            shootDictionary.Add(localFire, localFire.main.startColor);
        }
    }

    public void OnDamage(PlayerHit.Location location)
    {
        if (PlayerHit.Location.LEFT.Equals(location))
        {
            LeftSmoke.Play();
        }
        else if (PlayerHit.Location.RIGHT.Equals(location))
        {
            RightSmoke.Play();
        }
        else
        {
            CenterSmoke.Play();
        }
    }

    public void ResetSmokes()
    {
        LeftSmoke.Stop();
        RightSmoke.Stop();
        CenterSmoke.Stop();
    }

    public void TurnOnEngine()
    {
        FireThrust.Play();
    }

    public void OnDoubleDamage()
    {
        foreach (ParticleSystem localFire in Fire)
        {
            var main = localFire.main;
            main.startColor = new MinMaxGradient(Color.red);
        }
    }

    public void OnDoubleDamageOff()
    {
        foreach (ParticleSystem localFire in Fire)
        {
            var main = localFire.main;
            main.startColor = shootDictionary[localFire];
        }
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
