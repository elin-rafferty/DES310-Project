using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlsMenu : MonoBehaviour
{
    // Menu objects
    private EventSystem eventSystem;
    [SerializeField] private GameObject optionsMenu, controlsMenu;
    [SerializeField] private GameObject backButton, controlsButton;

    void Start()
    {
        eventSystem = FindAnyObjectByType<EventSystem>();
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
