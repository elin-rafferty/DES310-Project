using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public PlayerMovement player;

    private Vector3 offset = new Vector3(0, 0, -10);
    private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        Vector3 targetPosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
