using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterVentText : MonoBehaviour
{
    [SerializeField] SettingsSO settings;

    void OnEnable()
    {
        Text text = GetComponent<Text>();

        text.text = "- Approach Vent\n- Press " + (settings.Controls == 0 ? "E" : "A") + " to Enter Vent";
    }
}
