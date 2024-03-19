using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    [SerializeField] ActiveBuffs activeBuffs;
    [SerializeField] EventHandler eventHandler;
    [SerializeField] float slowedTimescale = 0.5f;
    
    bool isFrozen = false;

    private void Awake()
    {
        eventHandler.TimescaleFreeze.AddListener(TimescaleChangeResponse);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            activeBuffs.ReduceTimers(Time.deltaTime);
            Time.timeScale = activeBuffs.IsBuffActive(BuffType.SLOW_TIME) ? slowedTimescale : 1f;
        }
        GameObject.FindGameObjectWithTag("Music Manager").GetComponent<AudioSource>().pitch = activeBuffs.IsBuffActive(BuffType.SLOW_TIME) ? slowedTimescale : 1f;
    }

    void TimescaleChangeResponse(bool frozen)
    {
        isFrozen = frozen;
    }
}
