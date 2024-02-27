using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModifierScreen : MonoBehaviour
{
    [SerializeField] EventHandler eventHandler;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] ModifierBehaviour modifierBehaviour;
    [SerializeField] PersistentVariables persistentVariables;


    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;

        // Default Modifier Behaviour
        modifierBehaviour.spawnPercentChance = 50;
        modifierBehaviour.enemyHealthMultiplier = 1;
        modifierBehaviour.enemyDamageMultiplier = 1;
        modifierBehaviour.enemySpeedMultiplier = 1;
        modifierBehaviour.enemyAggroRangeMultiplier = 1;


        switch (persistentVariables.modifier) 
        {
            case Modifiers.ENEMY_HEALTH:
                // Set Modifier
                modifierBehaviour.enemyHealthMultiplier = 1.1f;
                // Set Text
                break;

            case Modifiers.ENEMY_DAMAGE:
                // Set Modifier
                modifierBehaviour.enemyDamageMultiplier = 1.05f;
                // Set Text
                break;

            case Modifiers.WALKER_SPEED:
                // Set Modifier
                modifierBehaviour.enemySpeedMultiplier = 1.1f;
                // Set Text
                break;

            case Modifiers.SPITTER_FIRE_RATE:
                // Set Modifier
                // Set Text
                break;

            case Modifiers.SPRINTER_SPEED:
                // Set Modifier
                // Set Text
                break;

            case Modifiers.RESOURCE_DROPRATE:
                // Set Modifier
                // Set Text
                break;
        }
        text.text = persistentVariables.modifier.ToString();
    }
}
