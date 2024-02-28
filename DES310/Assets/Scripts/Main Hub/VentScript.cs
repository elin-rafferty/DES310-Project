using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VentScript : MonoBehaviour
{
    public string sceneName;
    public float timeBeforeOpen = 2;
    public PersistentVariables persistentVariables;
    public InputManager inputManager;
    private bool trigger = false;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Canvas canvas;
    [SerializeField] Text text;
    [SerializeField] SettingsSO settings;

    private void Start()
    {
        text.text = "Press " + (settings.Controls == 0 ? "E" : "X") + " to enter";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger true on collision
            trigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set trigger false on collision end
            trigger = false;
        }
    }

    void Update()
    {
        if (timeBeforeOpen > 0)
        {
            timeBeforeOpen -= Time.deltaTime;
            if (timeBeforeOpen < 0)
            {
                timeBeforeOpen = 0;
            }
        }
        canvas.gameObject.SetActive(trigger && timeBeforeOpen == 0);
        // Load new scene
        if (inputManager.GetButtonDown("Interact") && trigger && timeBeforeOpen == 0)
        {
            // Modifier Load Screen
            if (sceneName != "Main Hub" && loadingScreen)
            {
                Modifiers newModifier;
                bool found;
                do
                {
                    found = false;
                    newModifier = (Modifiers)Random.Range(1, 6);

                    foreach (Modifiers m in persistentVariables.modifier)
                    {
                        if (m == newModifier)
                        {
                            found = true;
                        }
                    }
                }
                while (found == true || persistentVariables.modifier.Count == 7);
                persistentVariables.modifier.Add(newModifier);
                loadingScreen.SetActive(true);
            }
            persistentVariables.exitReason = LevelExitReason.VENT_EXIT;
            SceneManager.LoadScene(sceneName);
        }
    }
}
