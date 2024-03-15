
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
    private float currentHealth;
    [SerializeField] private EventHandler eventHandler;
    [SerializeField] private ModifierBehaviour modifierBehaviour;

    float invinsibliltyTimer = 0; 

    public void Update()
    {
        invinsibliltyTimer += Time.deltaTime;
    }

    private void Start()
    {
        currentHealth = maxHealth;
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

    /*public void Reduce(int damage)
    {
        slider.value = slider.value - 5f;
        Debug.Log("I got attacked for " + damage + " damage! My health is now " + slider.value);
        if (slider.value <= 0)
        {
            Die();
        }
    }*/

    /*public void AddHealth(int addHealth)
    {
        slider.value = slider.value + 5f;
        Debug.Log("I ate and got " + addHealth + " health back! My health is now " + slider.value);

    }*/

    private void Die()
    {
        Debug.Log("Died");
    }


    public void Damage(float damage)
    {
        // Play Sound
        SoundManager.instance.PlaySound(SoundManager.SFX.PlayerHit, transform, 0.8f);

        currentHealth -= damage * modifierBehaviour.enemyDamageMultiplier;
        eventHandler.PlayerHealthChange.Invoke(currentHealth);

        if (currentHealth < 20)
        {
            SoundManager.instance.PlaySound(SoundManager.SFX.PlayerHPLow, transform, 1f);
        }
        if (currentHealth <= 0)
        {
            eventHandler.PlayerDeath.Invoke();
        } else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            eventHandler.PlayerHealthChange.Invoke(currentHealth);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (invinsibliltyTimer >= 0.2f)
            {
                Damage(collision.gameObject.GetComponent<EnemyBase>().meleeDamage);
                invinsibliltyTimer = 0;
            }
        }
    }

}