using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private EventHandler eventHandler;
    [SerializeField] private List<AudioClip> sounds =  new List<AudioClip>();
    [SerializeField] private List<String> soundNames = new List<String>();
    public List<Tuple<String, AudioClip>> soundRegistry = new List<Tuple<string, AudioClip>>();

    // Start is called before the first frame update
    void Start()
    {
        eventHandler.PlaySound.AddListener(PlaySound);
        RegisterSounds();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            eventHandler.PlaySound.Invoke("TestSound");
        }
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

    void PlaySound(string soundName)
    {
        Debug.Log("reached playsound");
        foreach (Tuple<string, AudioClip> sound in soundRegistry)
        {
            if (sound.Item1 == soundName)
            {
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.clip = sound.Item2;
                audioSource.Play();
                Debug.Log("Playing sound " + soundName);
            }
        }
    }
}
