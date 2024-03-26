using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitShopButtonHandler : MonoBehaviour
{
    public GameObject exitButton;
    int framesTilButtonAppears = 0;
    private void OnEnable()
    {
        framesTilButtonAppears = 2;
    }

    private void OnDisable()
    {
        exitButton.GetComponent<Button>().enabled = false;
    }

    private void Update()
    {
        if (framesTilButtonAppears > 0)
        {
            framesTilButtonAppears--;
            if (framesTilButtonAppears == 0)
            {
                exitButton.GetComponent<Button>().enabled = true;
            }
        }
    }
}
