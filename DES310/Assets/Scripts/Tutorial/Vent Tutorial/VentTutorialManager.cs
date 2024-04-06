using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentTutorialManager : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] InputManager inputManager;
    [SerializeField] SettingsSO settings;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject playerVirtCam;
    [SerializeField] GameObject ventVirtCam;

    [SerializeField] Canvas ventCanvas;
    [SerializeField] Text ventText, ventOpenText;
    [SerializeField] VentScript vent;

    float timer;
    private void OnEnable()
    {
        timer = 1;

        playerMovement.GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
        playerMovement.enabled = false;

        ventCanvas.gameObject.SetActive(true);
        ventText.gameObject.SetActive(true);
        ventVirtCam.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.GetButtonDown("Interact") && timer < 0)
        {
            ventOpenText.gameObject.SetActive(true);
            ventText.gameObject.SetActive(false);
            ventVirtCam.SetActive(false);

            // Open vent
            vent.timeBeforeOpen = 0;

            playerMovement.enabled = true;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
