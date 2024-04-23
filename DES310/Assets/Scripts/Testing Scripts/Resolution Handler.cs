using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionHandler : MonoBehaviour
{
    [SerializeField] PersistentVariables persistentVariables;
    void Start()
    {
        if (!persistentVariables.hasForcedResolution)
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreenMode);
            persistentVariables.hasForcedResolution = true;
        }
    }
}
