using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounter : MonoBehaviour
{
    [SerializeField] GameObject boss; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss Trigger"))
        {
            Debug.Log("Trigger");
            // Close Door


            // Set Boss Active
            boss.SetActive(true);


            // Camera Sequence
        }
    }
}
