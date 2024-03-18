using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource soundObject;
    [SerializeField] private SFXClips[] audioClips;

    [System.Serializable]
    public class SFXClips
    {
        public SFX SoundName;
        public AudioClip Clip;
    }

    public enum SFX
    {
        PlayerShoot,
        LaserRebound,
        Overheat,
        HighHeat,
        PlayerHit,
        PlayerHPLow,

        MetalFootsteps1,
        MetalFootsteps2,
        MetalFootsteps3,
        MetalFootsteps4,

        GlassFootsteps1,
        GlassFootsteps2,

        ItemPickUp,
        ButtonSelect,
        CrateOpen,

        SpitterAttack,
        EnemyScreech,
        EnemyHit,
        EnemyDeath,

        PurchaseSuccessful,
        PurchaseUnsuccessful,

        DoorOpen,
        DoorClosed
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayAudioClip(AudioClip clip, Transform transform, float volume)
    {
        // Spawn Object
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);

        // Assign Audio Clip
        audioSource.clip = clip;

        // Change Volume
        audioSource.volume = volume;

        // Play Audio
        audioSource.Play();

        // destroy object on clip end
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlaySound(SFX audioClip, Transform transform, float volume)
    {
        // Spawn Object
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);

        // Assign Audio Clip
        audioSource.clip = GetAudioClip(audioClip);

        // Change Volume
        audioSource.volume = volume;

        // Play Audio
        audioSource.Play();

        // destroy object on clip end
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSound(SFX[] audioClip, Transform transform, float volume)
    {
        // Get random clip
        int rand = Random.Range(0, audioClip.Length);

        // Spawn Object
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);

        // Assign Audio Clip
        audioSource.clip = GetAudioClip(audioClip[rand]);

        // Change Volume
        audioSource.volume = volume;

        // Play Audio
        audioSource.Play();

        // Destroy object on clip end
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    private AudioClip GetAudioClip(SFX sound)
    {
        foreach(SFXClips clip in audioClips)
        {
            if(clip.SoundName == sound)
            {
                return clip.Clip;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return null;
    }
}
