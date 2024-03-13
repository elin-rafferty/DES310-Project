using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    public Rigidbody2D rb;

    private Vector3 offset = new Vector3(0, 0, -10);
    private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Fixed camera follow
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        // Delayed camera follow
        Vector3 targetPosition = ((Vector3)rb.position + offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
