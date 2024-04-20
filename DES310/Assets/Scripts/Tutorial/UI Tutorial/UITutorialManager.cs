using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorialManager : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] InputManager inputManager;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject goodWorkCanvas, overheatCanvas, oxygenCanvas;
    GameObject enemy;

    [SerializeField] GameObject oxygenPrefab;
    Vector3 enemyTransform;
    

    int tutorialStage = 0;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update()
    {
        switch (tutorialStage)
        {
            case 0:
                // Wait for enemy death
                if (enemy == null)
                {
                    // Force Drop 02
                    Instantiate(oxygenPrefab, enemyTransform, Quaternion.identity);

                    playerMovement.GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
                    playerMovement.enabled = false;
                    tutorialStage++;
                }
                else
                {
                    enemyTransform = enemy.transform.position;
                }
                break;

            case 1:
                // Good work text
                goodWorkCanvas.SetActive(true);

                if (inputManager.GetButtonDown("Interact"))
                {
                    goodWorkCanvas.SetActive(false);
                    tutorialStage++;
                }

                break;

            case 2:
                // Overheat explanation
                overheatCanvas.SetActive(true);

                if (inputManager.GetButtonDown("Interact"))
                {
                    overheatCanvas.SetActive(false);
                    tutorialStage++;
                }

                break;

            case 3:
                // Oxygen Explanation
                oxygenCanvas.SetActive(true);

                if (inputManager.GetButtonDown("Interact"))
                {
                    oxygenCanvas.SetActive(false);
                    tutorialStage++;
                }

                break;
            case 4:
                // Increment Tutorial Stage
                playerMovement.enabled = true;
                tutorialManager.IncrementTutorialStage();
                break;
        }
    }
}
