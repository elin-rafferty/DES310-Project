using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    [SerializeField] ActiveBuffs activeBuffs;
    [SerializeField] float slowedTimescale = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activeBuffs.ReduceTimers(Time.deltaTime);
        Time.timeScale = activeBuffs.IsBuffActive(BuffType.SLOW_TIME) ? slowedTimescale : 1f;
    }
}
