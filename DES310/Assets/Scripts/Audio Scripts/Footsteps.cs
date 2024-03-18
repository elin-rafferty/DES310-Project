using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private MapManager mapManager;
    [SerializeField] private Rigidbody2D rb;

    private float footstepDelay = 0.3f;
    private float footstepTimer = 0f;

    private TileType tileType = TileType.METAL;

    void Update()
    {
        if (footstepTimer >= footstepDelay && (rb.velocityY != 0 || rb.velocityX != 0))
        {
            SoundManager.instance.PlayAudioClip(mapManager.GetCurrentTileClip(transform.position), transform, 0.2f);

            footstepTimer = 0f;
        }
        else
        {
            footstepTimer += Time.deltaTime;
        }
    }
}
