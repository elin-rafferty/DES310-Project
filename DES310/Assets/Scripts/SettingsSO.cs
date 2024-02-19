using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavedSettings")]
public class SettingsSO : ScriptableObject
{
    public float MasterVolume = 0.1f;
    public float MusicVolume = 0.5f;
    public float SFXVolume = 0.5f;
    public int Graphics = 2;
}
