using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasterEgg : MonoBehaviour
{
    int count = 0;
    bool isRave = false;
    Image inky;
    float timer;
    float change = 0.2f;
    Color target;
    Color current;

    void Start()
    {
        inky = gameObject.GetComponent<Image>();
        count = 0;
        isRave = false;
        timer = change;
        target = Random.ColorHSV();
        current = inky.color;

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
                inky.color = Color.Lerp(new Color(target.r, target.g, target.b, 0.4f), new Color(current.r, current.g, current.b, 0.4f), timer/change);

                timer -= Time.unscaledDeltaTime;
            }
            else 
            {
                timer = change;
                current = target;
                target = Random.ColorHSV();
            }
        }
        else if(!isRave)
        {
            inky.color = new Color(1, 1, 1, 0.4f);
        }
    }
}
