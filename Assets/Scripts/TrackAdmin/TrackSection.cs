using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSection : MonoBehaviour
{
    [SerializeField] int sectionId;
    public MeteorTargetKeeper targetKeeper;
    private RaceTrackHandler rtHandler;

    void Start()
    {
        targetKeeper = transform.GetComponentInChildren<MeteorTargetKeeper>();
        targetKeeper.id = sectionId;
        rtHandler = FindObjectOfType<RaceTrackHandler>();
        SetStreetsId(sectionId);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CarController>())
        {
            rtHandler.SetSectionActive(sectionId);
            rtHandler.SetPlayerCheckpointPosition(other.transform.position);
        }
    }

    public int GetSectionId()
    {
        return sectionId;
    }

    private void SetStreetsId(int id)
    {
        foreach (Transform children in transform)
        {
            if(children.gameObject.layer == 9)
            {
                children.gameObject.AddComponent<TrackPartsIdHolder>();
                TrackPartsIdHolder childrenIdHolder = children.GetComponent<TrackPartsIdHolder>();
                childrenIdHolder.SetId(id);
            }
        }
    }

}
