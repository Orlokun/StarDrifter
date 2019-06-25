using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected UIController uiController;
    protected RaceTrackHandler trackHandler;

    protected virtual void Start()
    {
        uiController = FindObjectOfType<UIController>();
        trackHandler = FindObjectOfType<RaceTrackHandler>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (OtherIsPlayer(other))
        {
            ActivateItem();
            DeactivateMySelf();
        }
    }

    protected virtual void ActivateItem()
    {

    }

    protected virtual void ActivateItem(GameObject other)
    {

    }

    protected virtual void DeactivateMySelf()
    {
        trackHandler.SetItemActive(this, false);
    }

    protected virtual bool OtherIsPlayer(Collider _collider)
    {
        if (_collider.gameObject.GetComponent<CarController>())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
