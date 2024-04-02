using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCrateText : MonoBehaviour
{
    [SerializeField] SettingsSO settings;

    void OnEnable()
    {
        Text text = GetComponent<Text>();

        text.text = "- Approach Crate\n- Press " + (settings.Controls == 0 ? "E" : "A") + " to Open Crate\n- Press " + (settings.Controls == 0 ? "E" : "A") + " to pick up loot";
    }
}
