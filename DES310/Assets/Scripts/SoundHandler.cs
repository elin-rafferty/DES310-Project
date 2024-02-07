using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{

    [SerializeField] private List<AudioClip> sounds =  new List<AudioClip>();
    [SerializeField] private List<String> soundNames = new List<String>();
    public List<Tuple<String, AudioClip>> soundRegistry;

    // Start is called before the first frame update
    void Start()
    {
        RegisterSounds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RegisterSounds()
    {
        if (sounds.Count != soundNames.Count)
        {
            Debug.Log("Sound lists are incorrectly set up");
        } else
        {
            for (int i = 0; i < soundNames.Count; i++)
            {
                RegisterSound(soundNames[i], sounds[i]);
            }
        }
    }

    void RegisterSound(string soundName, AudioClip clip)
    {
        soundRegistry.Add(new Tuple<string, AudioClip>(soundName, clip));
    }
}
