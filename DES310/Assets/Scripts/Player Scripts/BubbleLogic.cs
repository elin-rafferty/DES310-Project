using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLogic : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] private float destroyTime;
    private float destroyTimer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        destroyTimer = destroyTime;
    }

    void Update()
    {
        if (destroyTimer <= 0 )
        {
            Destroy(gameObject);
        }
        else
        {
            spriteRenderer.color = Color.Lerp(Color.clear, Color.white, destroyTimer);
            destroyTimer -= Time.deltaTime;
        }
    }
}
