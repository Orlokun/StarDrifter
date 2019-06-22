using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    string rTimerName = "runTimer";
    string iTimer = "initialTimer";

    [SerializeField] GameObject runTimer;
    [SerializeField] GameObject initialTimer;

    void Start()
    {
        SetStartConditions();
    }

    void SetStartConditions()
    {
        UiElementSwitch(rTimerName, false);
        UiElementSwitch(iTimer, true);
    }

    public void UiElementSwitch(string elementName, bool turnOn)
    {
        switch (elementName)
        {
            case "runTimer":
                SetRunTimerOnOff(turnOn);
                break;
            case "initialTimer":
                SetInintialTimerOnOff(turnOn);
                break;
        }
    }

    void SetRunTimerOnOff(bool turnOn)
    {
        runTimer.SetActive(turnOn);
        if (turnOn == true)
        {
            runTimer.GetComponent<Timer>().SetCanCount(turnOn);
        }
    }

    void SetInintialTimerOnOff(bool turnOn)
    {
        initialTimer.SetActive(turnOn);
    }


}
