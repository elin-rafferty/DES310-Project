using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private EventHandler eventHandler;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private OptionsMenu optionsMenu;
    [SerializeField] private ControlsMenu controlsMenu;
    [SerializeField] private InputManager inputManager;

    // Update is called once per frame
    void Update()
    {
        // Pause button to navigate back
        if (inputManager.GetButtonDown("Pause"))
        {
            if (controlsMenu.gameObject.activeSelf)
            {
                controlsMenu.OpenOptionsMenu();
            }
            else if (optionsMenu.gameObject.activeSelf)
            {
                optionsMenu.OpenMainMenu();
            }
            else if (pauseMenu)
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);

                if (!pauseMenu.activeSelf)
                {
                    Time.timeScale = 1.0f;
                    eventHandler.TimescaleFreeze.Invoke(false);
                }
            }
        }
    }
}
