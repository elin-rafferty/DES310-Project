using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] SettingsSO settings;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject playerVirtCam;
    [SerializeField] GameObject enemyVirtCam;

    [SerializeField] Canvas enemyTextCanvas;
    [SerializeField] Text enemyText;

    void Update()
    {
        if (inputManager.GetButtonDown("Interact"))
        {
            playerMovement.enabled = true;
            enemyVirtCam.SetActive(false);
            enemyTextCanvas.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set text
            enemyText.text = "Careful! Hostile up ahead. - Use " + (settings.Controls == 0 ? "Left Click" : "RT") + " to shoot. \n- Use " + (settings.Controls == 0 ? "Shift" : "LT") + " to dash. \n Press " + (settings.Controls == 0 ? "E" : "A") + " to continue";
            enemyTextCanvas.gameObject.SetActive(true);

            playerMovement.GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
            playerMovement.enabled = false;
            enemyVirtCam.SetActive(true);
        }
    }
}
