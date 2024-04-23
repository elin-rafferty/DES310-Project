using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerHandler : MonoBehaviour
{
    [SerializeField] EventHandler eventHandler;
    //[SerializeField] private Slider slider;
    ////[SerializeField] TextMeshProUGUI text;
    //private float timeLeft = 300;
    //bool runTimer = false;

    //// Start is called before the first frame update
    void Awake()
    {
        eventHandler.LevelEnter.AddListener(OnLevelEnter);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (runTimer)
    //    {
    //        timeLeft += Time.deltaTime;
    //        // Add 1 cause floor
    //        //text.text = "Time Remaining: " + ((int)timeLeft + 1);
    //        if (timeLeft <= 0)
    //        {
    //            eventHandler.PlayerDeath.Invoke();
    //        }
    //    }
    //}

    void OnLevelEnter()
    {
        runTimer = true;
    }

    public float timeLeft;
    public float maxTime = 100;
    public float duration = 1;
    public float initialTime = 120;

    public Slider slider;
    bool runTimer = false;
    // Use this for initialization
    private void Start()
    {
        timeLeft = initialTime;
        slider.value = timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0 && runTimer)
        {
            timeLeft -= Time.deltaTime;
            slider.value = timeLeft;
        }
        else
        {
            if (timeLeft <= 0)
            {
                eventHandler.PlayerDeath.Invoke();
            }

        }
    }
}
