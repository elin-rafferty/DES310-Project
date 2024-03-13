using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class Virtual_Mouse_UI : MonoBehaviour
{
    VirtualMouseInput virtualMouseInput;
    // Start is called before the first frame update
    void Awake()
    {
        virtualMouseInput = GetComponent<VirtualMouseInput>();
    }

    private void LateUpdate()
    {
        Vector2 virtualMousePos = virtualMouseInput.virtualMouse.position.value;
        virtualMousePos.x = Mathf.Clamp(virtualMousePos.x, 0f, Screen.width);
        virtualMousePos.y = Mathf.Clamp(virtualMousePos.y, 0f, Screen.height);
        InputState.Change(virtualMouseInput.virtualMouse.position, virtualMousePos);
    }
}
