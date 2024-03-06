using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerHandler : MonoBehaviour
{
    [SerializeField] EventHandler eventHandler;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] private Slider overheatSlider;
    private float timeLeft = 300;
    bool runTimer = false;
    private float overheatLevel;

    // Start is called before the first frame update
    void Awake()
    {
        eventHandler.LevelEnter.AddListener(OnLevelEnter);
    }

    // Update is called once per frame
    void Update()
    {
        overheatSlider.value = overheatLevel;
        if (runTimer)
        {
            timeLeft -= Time.deltaTime;
            // Add 1 cause floor
            text.text = "Time Remaining: " + ((int)timeLeft + 1);
            overheatSlider.value = overheatLevel;
            if (timeLeft <= 0)
            {
                eventHandler.PlayerDeath.Invoke();
            }
        }
    }

    void OnLevelEnter()
    {
        runTimer = true;
        overheatSlider.value = overheatLevel;
    }
}
