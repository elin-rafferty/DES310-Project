using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHubPlayerPosManager : MonoBehaviour
{
    [SerializeField] private PersistentVariables persistentVariables;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 pos = Vector2.zero;
        switch (persistentVariables.exitReason) {
            case LevelExitReason.DEATH:
                pos = new Vector2(-16, -0.5f);
                break;
            case LevelExitReason.VENT_EXIT:
                pos = new Vector2(14, -0.5f);
                break;
            case LevelExitReason.NONE:
                pos = new Vector2(-0.5f, -0.5f);
                break;
        }
        GameObject.FindGameObjectWithTag("Player").transform.position = pos;
        persistentVariables.exitReason = LevelExitReason.NONE;
    }
}
