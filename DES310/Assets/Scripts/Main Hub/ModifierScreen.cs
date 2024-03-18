using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifierScreen : MonoBehaviour
{
    [SerializeField] EventHandler eventHandler;
    [SerializeField] Text text;
    [SerializeField] ModifierBehaviour modifierBehaviour;
    [SerializeField] PersistentVariables persistentVariables;
    [SerializeField] Text[] modifierListText;
    InputManager inputManager;


    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (inputManager.GetButtonDown("Interact")) 
            {
                gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    private void OnEnable()
    {
        inputManager = GetComponent<InputManager>();
        Time.timeScale = 0.0f;

        // Default Modifier Behaviour
        modifierBehaviour.spawnPercentChance = 50;

        modifierBehaviour.enemyHealthMultiplier = 1;
        modifierBehaviour.sprinterHealthMultiplier = 1;
        modifierBehaviour.walkerHealthMultiplier = 1;
        modifierBehaviour.spitterHealthMultiplier = 1;

        modifierBehaviour.enemyDamageMultiplier = 1;
        modifierBehaviour.sprinterDamageMultiplier = 1;
        modifierBehaviour.walkerDamageMultiplier = 1;
        modifierBehaviour.spitterDamageMultiplier = 1;

        modifierBehaviour.enemySpeedMultiplier = 1;
        modifierBehaviour.sprinterSpeedMultiplier = 1;
        modifierBehaviour.walkerSpeedMultiplier = 1;

        modifierBehaviour.enemyAttackSpeedMultiplier = 1;
        modifierBehaviour.sprinterAttackSpeedMultiplier = 1;
        modifierBehaviour.walkerAttackSpeedMultiplier = 1;
        modifierBehaviour.spitterAttackSpeedMultiplier = 1;

        modifierBehaviour.enemyAttackRangeMultiplier = 1;
        modifierBehaviour.sprinterAttackRangeMultiplier = 1;
        modifierBehaviour.walkerAttackRangeMultiplier = 1;
        modifierBehaviour.spitterAttackRangeMultiplier = 1;

        modifierBehaviour.enemyAggroRangeMultiplier = 1;
        modifierBehaviour.sprinterAggroRangeMultiplier = 1;
        modifierBehaviour.walkerAggroRangeMultiplier = 1;
        modifierBehaviour.spitterAggroRangeMultiplier = 1;


        for (int i = 0; i < persistentVariables.modifier.Count; i++)
        {
            switch (persistentVariables.modifier[i])
            {
                case Modifiers.ENEMY_HEALTH:
                    // Set Modifier
                    modifierBehaviour.enemyHealthMultiplier += 0.2f;
                    // Set Text
                    text.text = "All Enemy Health +20%";
                    break;

                case Modifiers.ENEMY_DAMAGE:
                    // Set Modifier
                    modifierBehaviour.enemyDamageMultiplier += 0.1f;
                    // Set Text
                    text.text = "All Enemy Damage +10%";
                    break;

                case Modifiers.WALKER_SPEED:
                    // Set Modifier
                    modifierBehaviour.walkerSpeedMultiplier += 0.2f;
                    // Set Text
                    text.text = "Walker Speed +20%";
                    break;

                case Modifiers.SPRINTER_SPEED:
                    // Set Modifier
                    modifierBehaviour.sprinterSpeedMultiplier += 0.1f;
                    // Set Text
                    text.text = "Sprinter Speed +10%";
                    break;

                case Modifiers.SPITTER_FIRE_RATE:
                    // Set Modifier
                    modifierBehaviour.spitterAttackSpeedMultiplier += 0.2f;
                    // Set Text
                    text.text = "Spitter Fire Rate +20%";
                    break;

                case Modifiers.SPITTER_RANGE:
                    // Set Modifier
                    modifierBehaviour.spitterAttackRangeMultiplier += 0.1f;
                    // Set Text
                    text.text = "Spitter Range +10%";
                    break;

                case Modifiers.RESOURCE_DROPRATE:
                    // Set Modifier
                    // Set Text
                    text.text = "Resource Droprate +20%";
                    break;
            }
        }

        // Modifier Text
        modifierListText[0].text = modifierListText[0].name + ": " + modifierBehaviour.enemyHealthMultiplier * 100 + "%";
        modifierListText[1].text = modifierListText[1].name + ": " + modifierBehaviour.enemyDamageMultiplier * 100 + "%";
        modifierListText[2].text = modifierListText[2].name + ": " + modifierBehaviour.walkerSpeedMultiplier * 100 + "%";
        modifierListText[3].text = modifierListText[3].name + ": " + modifierBehaviour.sprinterSpeedMultiplier * 100 + "%";
        modifierListText[4].text = modifierListText[4].name + ": " + modifierBehaviour.spitterAttackSpeedMultiplier * 100 + "%";
        modifierListText[5].text = modifierListText[5].name + ": " + modifierBehaviour.spitterAttackRangeMultiplier * 100 + "%";
    }
}
