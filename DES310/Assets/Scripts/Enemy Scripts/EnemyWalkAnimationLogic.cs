using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkAnimationLogic : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Velocity", rb.velocity.magnitude);
    }
}
