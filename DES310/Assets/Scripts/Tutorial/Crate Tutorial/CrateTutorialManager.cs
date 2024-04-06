using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrateTutorialManager : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] InputManager inputManager;
    [SerializeField] SettingsSO settings;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject playerVirtCam;
    [SerializeField] GameObject crateVirtCam;

    [SerializeField] Canvas crateCanvas;
    [SerializeField] Text crateText, crateOpenText;

    float timer;
    int count;

    private void OnEnable()
    {
        timer = 1;
        count = 0;

        playerMovement.GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
        playerMovement.enabled = false;

        crateCanvas.gameObject.SetActive(true);
        crateText.gameObject.SetActive(true);
        crateVirtCam.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.GetButtonDown("Interact") && timer < 0)
        {
            if (!GameObject.FindGameObjectWithTag("Item") && playerMovement.enabled == true)
            {
                if (count == 1)
                {
                    crateOpenText.gameObject.SetActive(false);
                    crateCanvas.gameObject.SetActive(false);

                    tutorialManager.IncrementTutorialStage();
                    return;
                }
            }
            else if (GameObject.FindGameObjectWithTag("Item") && playerMovement.enabled == true)
            {
                if (count == 0)
                {
                    count++;
                }
            }

            crateOpenText.gameObject.SetActive(true);
            crateText.gameObject.SetActive(false);
            crateVirtCam.SetActive(false);

            playerMovement.enabled = true;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
