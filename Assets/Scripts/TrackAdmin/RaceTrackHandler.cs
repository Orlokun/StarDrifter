using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceTrackHandler : MonoBehaviour
{
    [SerializeField] TrackSection[] tracks;

    private Vector3 playerCheckpointPosition;
    private List<Item> items;
    public string sectionsName;
    private int activeSectionId = 0;

    private void Start()
    {
        GetTracks();
        GetItems();
        playerCheckpointPosition = FindObjectOfType<CarController>().transform.position;
        LevelManager.SetRaceTrackHandler(this);
        StartCoroutine(LevelManager.TurnInitialTimerOn());
    }

    void GetItems()
    {
        items = new List<Item>();
        foreach (Item _item in FindObjectsOfType<Item>())
        {
            items.Add(_item);
        }
    }

    public void SetItemActive(Item _item, bool isActive)
    {
        if(items.Contains(_item))
        {
            _item.gameObject.SetActive(isActive);
        }
    }

    public void ResetItemsInLap()
    {
        foreach(Item _item in items)
        {
            _item.gameObject.SetActive(true);
        }
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

    public void SetSectionActive(int _activeSection)
    {
        if (activeSectionId != _activeSection)
        {
            activeSectionId = _activeSection;
            Debug.Log("Active Section is: " + activeSectionId);
        }
    }

    public void Restart()
    {
        StartCoroutine(Restarting());
    }

    private IEnumerator Restarting()
    {
        yield return new WaitForSeconds(3);
        Scene scene = SceneManager.GetActiveScene();
        int actualScene = scene.buildIndex;
        SceneManager.LoadScene(actualScene);
    }

    public void SetPlayerCheckpointPosition(Vector3 position)
    {
        position = new Vector3(position.x, position.y + 2, position.z);
        playerCheckpointPosition = position;
    }

    public Vector3 CheckpointPosition()
    {
        return playerCheckpointPosition;
    }
}
