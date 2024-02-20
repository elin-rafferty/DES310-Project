using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PersistentVariables persistentVariables;
    private void Start()
    {
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        persistentVariables.exitReason = LevelExitReason.NONE;
        SceneManager.LoadScene("Main Hub");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
