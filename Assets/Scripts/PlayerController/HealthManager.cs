using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private HealthBar hBar; 
    private int meteorHealth;
    private int playerHealth;
    private int difficulty;

    
    private void Awake()
    {
        hBar = FindObjectOfType<HealthBar>();
        difficulty = LevelManager.GetDifficulty();
        SetHealthData(difficulty);
    }


    public float CalculateHealthBar(float currentHealth, float maxHealth)
    {
        return (float)currentHealth / maxHealth;
    }

    public int AssignStartHealth(string objectType)
    {
        switch (objectType)
        {
            case "Asteroid":
                return meteorHealth;
            case "Player":
                return playerHealth;
            default:
                return 0;
        }
    }

    public void SetHealthData(int difficulty)
    {
        switch(difficulty)
        {
            case 1:
                SetHealthDifficultyOne();
                break;
            case 2:
                SetHealthDifficultyTwo();
                break;
            case 3:
                SetHealthDifficultyThree();
                break;
        }
    }

    public void SetHealthDifficultyOne()
    {
        playerHealth = 100;
        meteorHealth = 20;
    }

    public void SetHealthDifficultyTwo()
    {
        meteorHealth = 40;
        playerHealth = 100;

    }

    public void SetHealthDifficultyThree()
    {
        meteorHealth = 50;
        playerHealth = 100;

    }

    public void GiveDamage(GameObject dReceiver, int deltaDamage)
    {
        if (dReceiver.GetComponent<CarController>() || dReceiver.GetComponent<AsteroidController>())
        {
            hBar.TakeHit(deltaDamage);
        }
    }

    public void AddHealth(GameObject dReceiver, int health)
    {
        if (dReceiver.GetComponent<CarController>())
        {
            hBar.GiveHealth(health);
        }
    }
}
