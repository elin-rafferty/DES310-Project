using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    private bool trigger = false;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
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
        // Load chest menu
        if (inputManager.GetButtonDown("Interact") && trigger == true)
        {
            // Put code to do that here
            Debug.Log("Chest will open here eventually");
        }
    }
}
