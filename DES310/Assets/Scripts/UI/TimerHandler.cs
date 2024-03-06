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
    private float timeLeft = 300;
    bool runTimer = false;

    // Start is called before the first frame update
    void Awake()
    {
        eventHandler.LevelEnter.AddListener(OnLevelEnter);
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimer)
        {
            timeLeft -= Time.deltaTime;
            // Add 1 cause floor
            //text.text = "Time Remaining: " + ((int)timeLeft + 1);
            if (timeLeft <= 0)
            {
                eventHandler.PlayerDeath.Invoke();
            }
        }
    }

    void OnLevelEnter()
    {
        runTimer = true;
    }
}
