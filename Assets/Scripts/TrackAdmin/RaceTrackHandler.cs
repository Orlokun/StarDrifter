using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrackHandler : MonoBehaviour
{
    [SerializeField] TrackSection[] tracks;
    public string sectionsName;
    private int activeSectionId = 0;

    private void Start()
    {
        GetTracks();
    }

    void GetTracks()
    {
        for (int i = 0; i < tracks.Length; i++)
        {
            tracks[i] = GameObject.Find(sectionsName + i).GetComponent<TrackSection>();
        }
    }

    public int GetActiveSection()
    {
        return activeSectionId;
    }

    public void SetACtiveSection(int _activeSection)
    {
        if (activeSectionId != _activeSection)
        {
            activeSectionId = _activeSection;
            Debug.Log("Active Section is: " + activeSectionId);
        }
    }
}
