using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetAppropriatePromptText : MonoBehaviour
{
    [SerializeField] SettingsSO settings;
    [SerializeField] Text text;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != ("Main Hub"))
        {
            text.text = settings.Controls == 0 ? "Press E To Continue" : "Press A To Continue";
        }
    }
}
