using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounter : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] HorizontalDoor door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger");
            // Close Door


            // Set Boss Active
            boss.SetActive(true);
            door.Lock();
            door.Close();

            // Camera Sequence
        }
    }
}
