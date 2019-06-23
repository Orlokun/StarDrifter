using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestifyLap : MonoBehaviour
{
    LapCounter lCounter;
    bool hasFinishedLap = true;
    bool lapWasMade = false;
    
    void Start()
    {
        lCounter = FindObjectOfType<LapCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarController>() && hasFinishedLap)
        {
            hasFinishedLap = false;
            lapWasMade = true;
            lCounter.LapWasMade(lapWasMade);
        }
    }

    public void LapCompleted()
    {
        hasFinishedLap = true;
        lapWasMade = false;
    }
}
