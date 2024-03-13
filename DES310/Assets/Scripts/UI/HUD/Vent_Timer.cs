using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Vent_Timer : MonoBehaviour
{
    public TextMeshProUGUI text;
    private Vent_Script vent;
    private bool textEnabled = false;
    public float colourChangeTime = 0.5f;
    private float colourChangeTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        vent = GetComponent<Vent_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vent.timeBeforeOpen == 0 && !textEnabled)
        {
            text.text = "Vent Open!!!!!";
            text.color = Color.red;
            colourChangeTimer = colourChangeTime;
            textEnabled = true;
        } else if (textEnabled)
        {
            colourChangeTimer -= Time.deltaTime;
            if (colourChangeTimer <= 0)
            {
                colourChangeTimer = colourChangeTime;
                text.color = text.color == Color.red ? Color.yellow : Color.red;
            }
        }
    }
}
