using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    RaceTrackHandler rHandler;

    private void Start()
    {
        rHandler = FindObjectOfType<RaceTrackHandler>();    
    }

    private int GetActiveTrackSection()
    {
        return rHandler.GetActiveSection();
    }
}
