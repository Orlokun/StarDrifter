using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPrevention : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<CarController>())
        {
            LevelManager.ResetFromLastCheckPoint(other.gameObject);
        }
    }   
}
