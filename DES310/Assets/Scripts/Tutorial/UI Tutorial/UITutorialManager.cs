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
                    playerMovement.GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
                    playerMovement.enabled = false;
                    tutorialStage++;
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
