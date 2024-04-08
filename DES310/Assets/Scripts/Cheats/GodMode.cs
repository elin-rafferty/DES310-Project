using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMode : MonoBehaviour
{
    [SerializeField] ModifierBehaviour modifierBehaviour;
    [SerializeField] StringDetector detector;

    float currentDamageMultiplier;

    private void Start()
    {
        currentDamageMultiplier = modifierBehaviour.enemyDamageMultiplier;
    }

    void Update()
    {
        if (detector.active)
        {
            modifierBehaviour.enemyDamageMultiplier = 0;
        }
        else
        {
            modifierBehaviour.enemyDamageMultiplier = currentDamageMultiplier;
        }
    }
}
