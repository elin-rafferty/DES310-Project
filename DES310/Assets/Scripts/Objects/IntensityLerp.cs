using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IntensityLerp : MonoBehaviour
{
    private float lerpTimer;
    [SerializeField] private bool lerp;
    [SerializeField] private float lerpTime;
    [SerializeField] private float endIntensity;
    private float startIntensity;

    Light2D lightIntensity;

    void Start()
    {
        lightIntensity = GetComponent<Light2D>();
        startIntensity = lightIntensity.intensity;
    }

    void Update()
    {
        if (lerp)
        {
            if (lerpTimer <= -lerpTime)
            {
                lerpTimer = lerpTime;
            }

            lightIntensity.intensity = Mathf.Lerp(endIntensity, startIntensity, Mathf.Abs(lerpTimer) / lerpTime);

            lerpTimer -= Time.deltaTime;
        }
    }
}
