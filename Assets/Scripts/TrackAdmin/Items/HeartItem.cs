using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : Item
{
    [SerializeField] int lifeToGive = 10;

    protected override void Start()
    {
        base.Start();
    }

    protected override void ActivateItem()
    {
        base.ActivateItem();
        GameObject player = FindObjectOfType<CarController>().gameObject;
        HealthManager healthManager = FindObjectOfType<HealthManager>();
        healthManager.AddHealth(player, lifeToGive);
    }
}
