using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health_Handler : MonoBehaviour
{
    [SerializeField] EventHandler eventHandler;
    [SerializeField] Text text;
    private float health = 0;

    // Start is called before the first frame update
    void Awake()
    {
        eventHandler.PlayerHealthChange.AddListener(PlayerHealthChangeResponse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayerHealthChangeResponse(float newHealth)
    {
        health = newHealth;
        text.text = "" + Mathf.RoundToInt(health);
    }
}
