using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubReturnSounds : MonoBehaviour
{
    [SerializeField] private PersistentVariables persistentVariables;

    void Start()
    {
        if (persistentVariables.exitReason == LevelExitReason.VENT_EXIT)
        {
            SoundManager.instance.PlaySound(SoundManager.SFX.VentOpen, transform, 0.7f);
            SoundManager.instance.PlaySound(SoundManager.SFX.OxygenRefill, transform, 1f);
        }
    }
}
