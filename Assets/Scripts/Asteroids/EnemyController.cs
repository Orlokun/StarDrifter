using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(HealthBar))]

public class EnemyController : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    private string healthRecquired;

    private HealthManager hManager;
    private HealthBar healthBar;


    private int speedMovement = 6;

    private enum EnemyType
    {
        small, medium, big
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckComponentsAndVariables();
        healthBar = GetComponent<HealthBar>();
        
    }   

    private void CheckComponentsAndVariables()
    {
        healthRecquired = gameObject.tag;
        maxHealth = hManager.AssignStartHealth(healthRecquired);
    }

    public void ReceiveHit(int damage)  //Pensar dónde agregar la física del impacto
    {
        Debug.Log("Incoming damage is: " + damage);
        healthBar.TakeHit(damage);
    }

}
