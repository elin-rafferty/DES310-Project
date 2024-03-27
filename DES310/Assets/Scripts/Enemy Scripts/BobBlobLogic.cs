using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobBlobLogic : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    [SerializeField] private float damagePerSecond;

    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().Damage(damagePerSecond * Time.deltaTime);
        }
    }
}
