using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Set_Appropriate_Prompt_Text : MonoBehaviour
{
    [SerializeField] Settings_SO settings;
    [SerializeField] Text text;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != ("Main Hub"))
        {
            text.text = settings.Controls == 0 ? "Press E To Continue" : "Press X To Continue";
        }
    }
}
