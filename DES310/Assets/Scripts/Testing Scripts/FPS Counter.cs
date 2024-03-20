using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] Text text;
    float timeSinceUpdate = 0;
    // Update is called once per frame
    void Update()
    {
        timeSinceUpdate += Time.deltaTime;
        if (timeSinceUpdate > 0.2)
        {
            text.text = "FPS: " + (1f / Time.deltaTime).ToString("0");
            timeSinceUpdate = 0;
        }
    }

}
