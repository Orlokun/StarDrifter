using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSection : MonoBehaviour
{
    [SerializeField] int sectionId;
    public MeteorTargetKeeper targetKeeper;
    private RaceTrackHandler rtHandler;
    private HealthManager hManager; 

    void Start()
    {
        targetKeeper = transform.GetComponentInChildren<MeteorTargetKeeper>();
        rtHandler = FindObjectOfType<RaceTrackHandler>();
        hManager = FindObjectOfType<HealthManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CarController>())
        {
            rtHandler.SetACtiveSection(sectionId);
            hManager.ChangeHP(other.gameObject, 25);
        }
    }

}
