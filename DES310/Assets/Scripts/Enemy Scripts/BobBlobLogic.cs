using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobBlobLogic : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    [SerializeField] private float damagePerSecond;
    private float timer;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = lifeSpan;
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
            collision.gameObject.GetComponent<Health>().Damage(damagePerSecond * Time.deltaTime);
        }
    }
}
