using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Hub_Player_Pos_Manager : MonoBehaviour
{
    [SerializeField] private Persistent_Variables persistentVariables;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 pos = Vector2.zero;
        switch (persistentVariables.exitReason) {
            case Level_Exit_Reason.DEATH:
                pos = new Vector2(-16, -0.5f);
                break;
            case Level_Exit_Reason.VENT_EXIT:
                pos = new Vector2(14, -0.5f);
                break;
            case Level_Exit_Reason.NONE:
                pos = new Vector2(-0.5f, -0.5f);
                break;
        }
        GameObject.FindGameObjectWithTag("Player").transform.position = pos;
        persistentVariables.exitReason = Level_Exit_Reason.NONE;
    }
}
