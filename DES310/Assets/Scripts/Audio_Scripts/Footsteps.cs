using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    public enum TileType
    {
        METAL,
        GLASS
    }

    [SerializeField] private Sound_Manager.SFX[] metalFootsteps;
    [SerializeField] private Sound_Manager.SFX[] glassFootsteps;
    [SerializeField] private Rigidbody2D rb;

    private float footstepDelay = 0.3f;
    private float footstepTimer = 0f;

    private TileType tileType = TileType.METAL;

    void Update()
    {
        if (footstepTimer >= footstepDelay && (rb.velocityY != 0 || rb.velocityX != 0))
        {
            switch (tileType)
            {
                case TileType.METAL:
                    PlayFootsteps(metalFootsteps);
                    break;

                case TileType.GLASS:
                    PlayFootsteps(glassFootsteps);
                    break;
            }
            footstepTimer = 0f;
        }
        else
        {
            footstepTimer += Time.deltaTime;
        }
    }

    void PlayFootsteps(Sound_Manager.SFX[] footstepClips)
    {
        if (footstepClips.Length > 0)
        {
            Sound_Manager.instance.PlayRandomSound(footstepClips, transform, 0.2f);
        }
    }
}
