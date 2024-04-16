using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private MapManager mapManager;
    [SerializeField] private Rigidbody2D rb;

    private float footstepDelay = 0.625f;
    private float footstepTimer = 0f;

    void Update()
    {
        if (footstepTimer >= footstepDelay && (rb.velocityY != 0 || rb.velocityX != 0))
        {
            AudioClip clip = mapManager.GetCurrentTileClip(transform.position);
            if (mapManager.GetTileType(transform.position) == TileType.METAL)
            {
                SoundManager.instance.PlayAudioClip(mapManager.GetCurrentTileClip(transform.position), transform, 0.2f);
            }
            else
            {
                SoundManager.instance.PlayAudioClip(mapManager.GetCurrentTileClip(transform.position), transform, 0.1f);
            }
            

            footstepTimer = 0f;
        }
        else
        {
            footstepTimer += Time.deltaTime;
        }
    }
}
