using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] Text PlayerScore;
    void Start()
    {
        PlayerScore.text = "0";
    }

    public void SetScore(int value)
    {
        PlayerScore.text = value.ToString();
    }

}
