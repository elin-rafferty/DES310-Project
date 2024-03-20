using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLegLogic : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    private float rotateSpeed;
    private float smoothTime = 0.05f;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", rb.velocity.magnitude);

        if (rb.velocity.magnitude > 0.01)
        {
            // Direction
            Vector2 direction = rb.velocity.normalized;

            // Angle;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref rotateSpeed, smoothTime);

            // Rotation
            transform.rotation = Quaternion.identity;
            transform.Rotate(Vector3.forward, angle);
        }
    }
}
