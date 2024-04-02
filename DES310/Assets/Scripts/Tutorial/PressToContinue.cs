using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressToContinue : MonoBehaviour
{
    [SerializeField] SettingsSO settings;
    [SerializeField] Text text;

    private void OnEnable()
    {
        text.text += "\nPress " + (settings.Controls == 0 ? "E" : "A") + " to continue.";
    }
}
