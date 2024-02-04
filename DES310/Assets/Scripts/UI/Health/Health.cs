
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
    [SerializeField] private Renderer render;
    public Slider slider;


    public void Update()
    {

    }

    private void Start()
    {
        currentHealth.Value = 100;
    }

    //public void Reduce(int damage)
    //{
    //    currentHealth.Value -= 5f;
    //    Debug.Log("I got attacked for " + damage + " damage! My health is now " + currentHealth.Value);
    //    if (currentHealth.Value <= 0)
    //    {
    //        Die();
    //    }
    //}

    public void Reduce(int damage)
    {
        slider.value = slider.value - 5f;
        Debug.Log("I got attacked for " + damage + " damage! My health is now " + slider.value);
        if (slider.value <= 0)
        {
            Die();
        }
    }

    public void AddHealth(int addHealth)
    {
        slider.value = slider.value + 5f;
        Debug.Log("I ate and got " + addHealth + " health back! My health is now " + slider.value);

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