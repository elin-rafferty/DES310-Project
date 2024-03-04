using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu, optionsMenu;
    [SerializeField] private PersistentVariables persistentVariables;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject playButton, optionsBackButton;

    private void OnEnable()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        eventSystem.SetSelectedGameObject(playButton);
        Time.timeScale = 1f;
    }

    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(optionsBackButton);
    }

    public void ReturnToMenu() 
    {
        persistentVariables.exitReason = LevelExitReason.NONE;
        SceneManager.LoadScene("Main Menu");
    }
}
