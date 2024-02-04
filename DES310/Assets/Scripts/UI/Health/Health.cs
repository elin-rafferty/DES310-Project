
using FloatData;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private FloatValueSO currentHealth;
    [SerializeField] private EventHandler eventHandler;
    //[SerializeField] private Renderer render;


    private void Start()
    {
        currentHealth.Value = 100;

    }

    public void Reduce(int damage)
    {
        currentHealth.Value -= 5f;
        Debug.Log("I got attacked for " + damage + " damage! My health is now " + currentHealth.Value);
        if (currentHealth.Value <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Died");
    }


    public void Damage(float damage)
    {

        currentHealth.Value -= damage;


        if (currentHealth.Value <= 0)
        {
            eventHandler.PlayerDeath.Invoke();
        }
    }


    
}