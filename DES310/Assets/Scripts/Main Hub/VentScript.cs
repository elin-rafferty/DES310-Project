using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VentScript : MonoBehaviour
{
    public string sceneName;
    public PersistentVariables persistentVariables;
    private bool trigger = false;
    [SerializeField] GameObject loadingScreen;

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
        // Load new scene
        if (Input.GetKeyDown(KeyCode.E) && trigger == true)
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
