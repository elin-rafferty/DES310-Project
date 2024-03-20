using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ElectricBoogaloo : MonoBehaviour
{
    int count = 0;
    bool isRave = false;
    Light2D playerLight;
    float timer;
    float change = 0.2f;
    Color target;
    Color current;

    void Start()
    {
        playerLight = gameObject.GetComponent<Light2D>();
        count = 0;
        isRave = false;
        timer = change;
        target = Random.ColorHSV();
        current = playerLight.color;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && count == 0)
        {
            count++;
        }
        else if (Input.GetKeyDown(KeyCode.O) && count > 0)
        {
            count++;
        }
        else if (Input.anyKeyDown)
        {
            count = 0;
        }

        if (count >= 3)
        {
            isRave = !isRave;
            count = 0;
        }

        if (isRave)
        {
            if (timer > 0)
            {
                playerLight.color = Color.Lerp(new Color(target.r, target.g, target.b, 0.4f), new Color(current.r, current.g, current.b, 0.4f), timer / change);

                timer -= Time.unscaledDeltaTime;
            }
            else
            {
                timer = change;
                current = target;
                target = Random.ColorHSV();
            }
        }
        else if (!isRave)
        {
            playerLight.color = new Color(1, 1, 1, 0.4f);
        }
    }
}