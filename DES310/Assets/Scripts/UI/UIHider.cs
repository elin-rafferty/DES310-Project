using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHider : MonoBehaviour
{
    [SerializeField] GameObject overheatSlider, overheatBars, baseHUD, timerSlider, image, health;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main Hub")
        {/*
            overheatSlider.SetActive(false);
            overheatBars.SetActive(false);
            baseHUD.SetActive(false);*/
            timerSlider.SetActive(false);
            image.SetActive(false);
            //health.SetActive(false);
        }
    }
}
