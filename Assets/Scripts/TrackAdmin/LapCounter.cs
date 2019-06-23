using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCounter : MonoBehaviour
{
    public float addTimeLap;
    UIController uiController;
    TestifyLap testifier;
    bool lapWasMade = false;
    public int numberOfLaps = 0;

    void Start()
    {
        uiController = FindObjectOfType<UIController>();
        testifier = FindObjectOfType<TestifyLap>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarController>())
        {
            if (lapWasMade)
            {
                AddNewLap();
                LevelManager.ChangeTimer(addTimeLap);
                testifier.LapCompleted();
                lapWasMade = false;
            }
        }
    }

    void AddNewLap()
    {
        numberOfLaps++;
        LevelManager.SetNewLap(numberOfLaps);
    }

    public void LapWasMade(bool testimony)
    {
        lapWasMade = testimony;
    }
}
