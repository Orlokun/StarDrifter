using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    string rTimerName = "runTimer";
    string iTimer = "initialTimer";

    [SerializeField] GameObject runTimer;
    [SerializeField] GameObject initialTimer;
    [SerializeField] TextMeshProUGUI lapCounter; 

    Timer rTimer;
    Timer inTimer;

    void Start()
    {
        DefineTimers();
        SetStartConditions();
    }

    void SetStartConditions()
    {
        UiElementSwitch(rTimerName, false);
        UiElementSwitch(iTimer, true);
        SetLapCounter(0);
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

    #region LapCounter
    public void SetLapCounter(int lapNumber)
    {
        lapCounter.text = lapNumber.ToString();
    }

    #endregion

    public void RestartToCheckpoint()
    {
        CarController player = FindObjectOfType<CarController>();
        LevelManager.ResetFromLastCheckPoint(player.gameObject);
    }


    #region Timers
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

    public Timer GetTimer(string neededTimer)
    {
        switch(neededTimer)
        {
            case "runTimer":
                return rTimer;
            case "initialTimer":
                return inTimer;
            default:
                return null;
        }
    }

    private void DefineTimers()
    {
        if (runTimer != null)
        {
            rTimer = runTimer.GetComponent<Timer>();
        }
        if (initialTimer != null)
        {
            inTimer = initialTimer.GetComponent<Timer>();
        }
    }
    #endregion
}
