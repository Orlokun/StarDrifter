﻿using System.Collections;
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
        maxHealth = hManager.AssignStartHealth(gameObject.tag);
    }   

    void Start()
    {
        currentHealth = maxHealth;
        slider.value = hManager.CalculateHealthBar(currentHealth, maxHealth);
    }

    public void TakeHit(int incomingDamage)
    {
        currentHealth -= incomingDamage;
        Debug.Log("Current Health is: " + currentHealth);
        slider.value = hManager.CalculateHealthBar(currentHealth, maxHealth);
    }



}
