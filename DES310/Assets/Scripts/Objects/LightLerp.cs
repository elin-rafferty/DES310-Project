using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightLerp : MonoBehaviour
{
    private float lerpTimer;
    [SerializeField] private bool lerp;
    [SerializeField] private float lerpTime;
    [SerializeField] private Color endColour;
    private Color startColour;

    Light2D lightColour;

    void Start()
    {
        lightColour = GetComponent<Light2D>();
        startColour = lightColour.color;
    }

    void Update()
    {
        if (lerp)
        {
            if (lerpTimer <= -lerpTime)
            {
                lerpTimer = lerpTime;
            }

            lightColour.color = Color.Lerp(endColour, startColour, Mathf.Abs(lerpTimer) / lerpTime);

            lerpTimer -= Time.deltaTime;
        }
    }
}
