using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_Trigger : MonoBehaviour
{
    public EventHandler eventHandler;

    // Start is called before the first frame update
    void Start()
    {
        eventHandler.LevelEnter.Invoke();
    }
}
