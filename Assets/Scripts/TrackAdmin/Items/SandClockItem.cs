using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandClockItem : Item
{
    [SerializeField] float timeToAdd;

    protected override void Start()
    {
        base.Start();
    }

    protected override void ActivateItem()
    {
        base.ActivateItem();
        Timer rTimer = uiController.GetTimer("runTimer");
        rTimer.ChangeTimer(timeToAdd);
    }
}
