using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    HealthManager hManager;
    private float currentHealth;
    int maxHealth;

    public GameObject healthBarUI;
    public Slider slider;

    private void Awake()
    {
        hManager = FindObjectOfType<HealthManager>();
    }   

    void Start()
    {
        maxHealth = hManager.AssignStartHealth(gameObject.tag);
        currentHealth = maxHealth;
        slider.value = hManager.CalculateHealthBar(currentHealth, maxHealth);
    }

    public void TakeHit(int incomingDamage)
    {
        currentHealth -= incomingDamage;
        Debug.Log("Current Health is: " + currentHealth);
        slider.value = hManager.CalculateHealthBar(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            LevelManager.YouLoose();
        }
    }

    public void GiveHealth(int incomingHealth)
    {
        currentHealth += incomingHealth;
        Debug.Log("Current Health is: " + currentHealth);
        slider.value = hManager.CalculateHealthBar(currentHealth, maxHealth);
    }



}
