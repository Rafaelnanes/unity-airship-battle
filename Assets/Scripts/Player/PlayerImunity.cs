using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImunity : MonoBehaviour
{
    [SerializeField] GameObject Shield;
    private PlayerActions playerActions;
    private bool imune;
    private float imunityTimeTriggered;

    private void Start()
    {
        playerActions = GetComponent<PlayerActions>();
    }

    public void Disable()
    {
        if (imune && Time.time - imunityTimeTriggered > playerActions.GetImunityTimeLimit())
        {
            imune = false;
            Shield.SetActive(false);
        }
    }

    public void Enable()
    {
        imune = true;
        Shield.SetActive(true);
        imunityTimeTriggered = Time.time;
    }

    public bool isImune()
    {
        return imune;
    }


}
