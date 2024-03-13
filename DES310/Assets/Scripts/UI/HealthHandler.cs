using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] EventHandler eventHandler;
    [SerializeField] Text text;
    private float health = 100;

    [SerializeField] private float colourTimer = 0.15f;
    Color redColor = new Color(0.839215f, 0.313725f, 0.266666f, 1f);
    Color defaultColor = new Color(0.1764706f, 0.6470588f, 0.227451f, 1f);

    // Start is called before the first frame update
    void Awake()
    {
        eventHandler.PlayerHealthChange.AddListener(PlayerHealthChangeResponse);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 20)
        {
            // Set health Colour
            if (colourTimer > 0 && colourTimer < 0.15)
            {
                text.color = Color.Lerp(defaultColor, redColor, colourTimer / 0.15f);
                colourTimer += Time.deltaTime;
            }
            else if (colourTimer >= 0.15)
            {
                colourTimer = -0.001f;
            }
            else if (colourTimer < 0 && colourTimer > -0.15)
            {
                text.color = Color.Lerp(redColor, defaultColor, Mathf.Abs(colourTimer / 0.15f));
                colourTimer -= Time.deltaTime;
            }
            else if (colourTimer < -0.15)
            {
                colourTimer = 0.001f;
            }
        }
        else
        {
            text.color = defaultColor;
        }
    }

    void PlayerHealthChangeResponse(float newHealth)
    {
        health = newHealth;
        text.text = "" + Mathf.RoundToInt(health);
    }
}
