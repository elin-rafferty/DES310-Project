using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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

    private void OnEnable()
    {
        timer = 1;

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
                crateOpenText.gameObject.SetActive(false);
                crateCanvas.gameObject.SetActive(false);

                tutorialManager.IncrementTutorialStage();
                return;
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
