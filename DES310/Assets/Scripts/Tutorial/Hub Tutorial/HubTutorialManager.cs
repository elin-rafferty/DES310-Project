using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubTutorialManager : MonoBehaviour
{
    [SerializeField] PersistentVariables persistentVariables;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] InputManager inputManager;

    [SerializeField] GameObject shopCanvas, storageCanvas, upgradeCanvas, ventCanvas, hubCanvas;
    [SerializeField] GameObject shopCam, storageCam, upgradeCam, ventCam;

    int tutorialStage = 0;

    void Start()
    {
        if (persistentVariables.exitReason != LevelExitReason.TUTORIAL)
        {
            gameObject.SetActive(false);
        }
        else
        {
            playerMovement.enabled = false;
        }
    }

    void Update()
    {
        switch (tutorialStage)
        {
            case 0:
                // Hub Description
                hubCanvas.SetActive(true);

                if (inputManager.GetButtonDown("Interact"))
                {
                    hubCanvas.SetActive(false);
                    tutorialStage++;
                }
                break;

            case 1:
                // Storage Description
                storageCanvas.SetActive(true);
                storageCam.SetActive(true);

                if (inputManager.GetButtonDown("Interact"))
                {
                    storageCanvas.SetActive(false);
                    storageCam.SetActive(false);
                    tutorialStage++;
                }

                break;

            case 2:
                // Shop Description
                shopCanvas.SetActive(true);
                shopCam.SetActive(true);

                if (inputManager.GetButtonDown("Interact"))
                {
                    shopCanvas.SetActive(false);
                    shopCam.SetActive(false);
                    tutorialStage++;
                }

                break;

            case 3:
                // Upgrade Description
                upgradeCanvas.SetActive(true);
                upgradeCam.SetActive(true);

                if (inputManager.GetButtonDown("Interact"))
                {
                    upgradeCanvas.SetActive(false);
                    upgradeCam.SetActive(false);
                    tutorialStage++;
                }

                break;

            case 4:
                // Vent Description
                ventCanvas.SetActive(true);
                ventCam.SetActive(true);

                if (inputManager.GetButtonDown("Interact"))
                {
                    ventCanvas.SetActive(false);
                    ventCam.SetActive(false);
                    tutorialStage++;
                }

                break;

            case 5:
                // Increment Tutorial Stage
                playerMovement.enabled = true;
                break;
        }
    }
}
