using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    // Menu objects
    private EventSystem eventSystem;
    [SerializeField] private SettingsSO settings;
    [SerializeField] private GameObject optionsMenu, controlsMenu;
    [SerializeField] private GameObject backButton, controlsButton;
    [SerializeField] private RawImage controllerImage, keyboardImage; 

    void Start()
    {
        eventSystem = FindAnyObjectByType<EventSystem>();
    }

    private void OnEnable()
    {
        if (settings.Controls == 0)
        {
            // M&K Image
            keyboardImage.enabled = true;
            controllerImage.enabled = false;
        }
        else 
        {
            // Controller Image
            controllerImage.enabled = true;
            keyboardImage.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            OpenOptionsMenu();
        }
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(backButton);
        }
    }

    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
        controlsMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(controlsButton);
    }

    public void PlaySelectSound()
    {
        SoundManager.instance.PlaySound(SoundManager.SFX.ButtonSelect, transform, 1f);
    }
}
