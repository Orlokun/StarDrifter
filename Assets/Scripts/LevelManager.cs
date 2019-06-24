using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static int difficulty = 2;
    private static bool canDrive;
    private static UIController uiController = FindObjectOfType<UIController>();
    private static MeteorAdmin mAdmin = FindObjectOfType<MeteorAdmin>();
    private static RaceTrackHandler trackHandler = FindObjectOfType<RaceTrackHandler>();
    private static Timer initialTimer; 

    public static void SetDifficulty(int incomingDif)
    {
        difficulty = incomingDif;
    }

    public static int GetDifficulty()
    {
        return difficulty;
    }

    public static void StartGame()
    {
        SetCanDrive(true);
        uiController.UiElementSwitch("initialTimer", false);
        uiController.UiElementSwitch("runTimer", true);
    }

    public static void SetCanDrive(bool _canDrive)
    {
        canDrive = _canDrive;
    }

    public static bool CanDrive()
    {
        return canDrive;
    }

    public static IEnumerator TurnInitialTimerOn()
    {
        yield return new WaitForSeconds(2);

        initialTimer.SetCanCount(true);
    }

    public static void SetInitialTimer(Timer _timer)
    {
        initialTimer = _timer;
    }

    public static void ResetFromLastCheckPoint(GameObject _object)
    {
        Rigidbody rBody = _object.GetComponent<Rigidbody>();
        rBody.velocity = new Vector3(0, 0, 0);
        _object.transform.position = trackHandler.CheckpointPosition();
    }

    public static void ChangeTimer(float timeLap)
    {
        Timer rTimer = uiController.GetTimer("runTimer");
        rTimer.ChangeTimer(timeLap);
    }

    public static void SetNewLap(int numberOfLaps)
    {
        trackHandler.ResetItemsInLap();
        uiController.SetLapCounter(numberOfLaps);
        mAdmin.SetNewFrequency(numberOfLaps);
    }
}
