using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class GoodWorkText : MonoBehaviour
{
    [SerializeField] SettingsSO settings;
    [SerializeField] Text text;

    void Start()
    {
        text.text = "Good shooting out there! \nPress " + (settings.Controls == 0 ? "E" : "A") + " to continue";
    }
}
