using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorialManager : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] InputManager inputManager;
    [SerializeField] SettingsSO settings;
    [SerializeField] GameObject enemyTrigger;

    void Update()
    {
        if (enemyTrigger == null) 
        { 
            tutorialManager.IncrementTutorialStage();
            gameObject.SetActive(false);
        }
    }
}
