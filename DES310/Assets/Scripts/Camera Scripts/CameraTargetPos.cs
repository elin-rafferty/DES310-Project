using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetPos : MonoBehaviour
{
    [SerializeField] private SettingsSO settings;

    private Vector2 lastAimPosition = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 lookDirection = lastAimPosition;
        //if (settings.Controls == 1)
        //{

        //    if (Input.GetAxis("Look X") != 0 || Input.GetAxis("Look Y") != 0)
        //    {
        //        // Get joystick input
        //        lookDirection = new Vector2(Input.GetAxis("Look X"), Input.GetAxis("Look Y"));
        //        lookDirection.Normalize();
        //    }
        //}
        //else
        //{
        //    // Get mouse direction 
        //    Vector2 mousePos = Input.mousePosition;
        //    Vector2 screenMiddle = new Vector2(Screen.width / 2, Screen.height / 2);
        //    Vector2 mouseDirection = mousePos - screenMiddle;
        //    lookDirection = mouseDirection;
        //    mouseDirection.Normalize();
        //}

        //lastAimPosition = lookDirection;

    }
}
