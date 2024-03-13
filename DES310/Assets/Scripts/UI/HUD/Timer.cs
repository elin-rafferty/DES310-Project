using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLeft;
    public float maxTime = 100f;
    //public float duration = 1;

    public Slider slider;

    // Use this for initialization
    private void Start()
    {
        timeLeft = maxTime;
        slider.value = timeLeft;
    }

    // Update is called once per frame
    void Update()
    {

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            slider.value = timeLeft / maxTime;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
