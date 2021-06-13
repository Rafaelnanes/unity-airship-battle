using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{

    [SerializeField] GameObject rightSmoke;
    [SerializeField] GameObject leftSmoke;
    [SerializeField] GameObject centerSmoke;

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
}
