using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VentScript : MonoBehaviour
{
    public string sceneName;
    public PersistentVariables persistentVariables;
    private bool trigger = false;

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
            persistentVariables.exitReason = LevelExitReason.VENT_EXIT;
            SceneManager.LoadScene(sceneName);
        }
    }
}
