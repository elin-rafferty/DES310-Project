using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Dictionary<string, bool> downLastFrame = new();
    Dictionary<string, bool> downThisFrame = new();
    [SerializeField] List<string> axesToCheck = new();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var axis in axesToCheck) { 
            downThisFrame.Add(axis, false);
        }
    }

    public void Update()
    {
        UpdateKeys();
    }

    public void UpdateKeys()
    {
        downLastFrame.Clear();
        foreach (var button in downThisFrame)
        {
            downLastFrame.Add(button.Key, button.Value);
        }
        downThisFrame.Clear();
        foreach(var axis in axesToCheck)
        {
            downThisFrame.Add(axis, Input.GetAxisRaw(axis) != 0 || Input.GetButton(axis));
        }
    }

    public bool GetButtonDown(string axis)
    {
        if (downThisFrame.ContainsKey(axis) && downLastFrame.ContainsKey(axis))
        {
            return downThisFrame[axis] && !downLastFrame[axis];
        }
        else return false;
    }

    public bool GetButton(string axis)
    {
        if (downThisFrame.ContainsKey(axis))
        {
            return downThisFrame[axis];
        }
        else return false;
    }
}
