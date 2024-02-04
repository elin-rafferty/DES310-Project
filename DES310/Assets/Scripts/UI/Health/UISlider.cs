
using FloatData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private FloatValueSO currentHealth;

    private float damage;
    private object modifierBehaviour;

    //public void Update()
    //{
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            slider.value = slider.value - 10;
    //            Debug.Log("10 health lost");
    //        }
    //}

    private void Die()
    {
        Debug.Log("Died");
    }


    public void Reduce(int damage)
    {
        slider.value = slider.value - 10;
        Debug.Log("I got attacked for " + damage + " damage! My health is now " + slider.value);
        if (slider.value <= 0)
        {
            Die();
        }
    }
}
