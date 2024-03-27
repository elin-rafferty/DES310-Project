using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobBlobLogic : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    [SerializeField] private float damagePerSecond;
    private float timer;
    private SpriteRenderer spriteRenderer;
    private float hitTimer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = lifeSpan;
        hitTimer = 1;
        Destroy(gameObject, lifeSpan);
    }

    void Update()
    {
        if (timer <= 1)
        {
            spriteRenderer.color = Color.Lerp(Color.clear, Color.white, timer);
        }

        timer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (hitTimer < 0)
            {
                collision.gameObject.GetComponent<Health>().Damage(damagePerSecond);
                hitTimer = 1;
            }

            hitTimer -= Time.deltaTime;
        }
    }
}
