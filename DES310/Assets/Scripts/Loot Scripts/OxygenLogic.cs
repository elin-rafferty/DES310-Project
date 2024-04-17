using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OxygenLogic : MonoBehaviour
{
    private TimerHandler timerHandler;
    public float replenishedOxygen = 0;

    private void Awake()
    {
        timerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<TimerHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // Add oxygen to tank
            timerHandler.timeLeft += replenishedOxygen;
            if (timerHandler.timeLeft > timerHandler.maxTime)
            {
                timerHandler.timeLeft = timerHandler.maxTime;
            }

            // Destroy on collect
            Destroy(gameObject);
        }
    }
}
