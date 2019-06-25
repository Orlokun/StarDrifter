using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager lManagerInstace;

    private static int difficulty;
    private static bool canDrive;
    private static UIController uiController = FindObjectOfType<UIController>();
    private static MeteorAdmin mAdmin = FindObjectOfType<MeteorAdmin>();
    private static RaceTrackHandler trackHandler = FindObjectOfType<RaceTrackHandler>();
    private static CarController cController = FindObjectOfType<CarController>();
    private static Timer initialTimer;
    private static int starTime;

    private void Awake()
    {
        if(lManagerInstace == null)
        {
            lManagerInstace = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

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
        initialTimer.SetTimer(starTime);
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

    public static int GetMainTimerTime()
    {
        return starTime;
    }

    public static void ResetFromLastCheckPoint(GameObject _object)
    {
        Rigidbody rBody = _object.GetComponent<Rigidbody>();
        rBody.velocity = new Vector3(0, 0, 0);
        rBody.rotation = new Quaternion(0, 0, 0, 1);
        _object.transform.position = trackHandler.CheckpointPosition();
    }

    public static void ChangeTimer(float timeLap)
    {
        Timer rTimer = uiController.GetTimer("runTimer");
        rTimer.AddToTimer(timeLap);
    }

    public static void SetInitialTimeAmount(int _time)
    {
        starTime = _time;
    }

    public static void SetNewLap(int numberOfLaps)
    {
        trackHandler.ResetItemsInLap();
        uiController.SetLapCounter(numberOfLaps);
        mAdmin.SetNewFrequency(numberOfLaps);
    }

    public static void LoadNextScene(int actualScene)
    {
        int sceneToLoad = actualScene + 1;
        SceneManager.LoadScene(sceneToLoad);
    }

    public static void YouLoose()
    {
        canDrive = false;
        TurnAudioOff();
        Debug.Log("YOU LOOSE");
        trackHandler.Restart();
    }

    private static void TurnAudioOff()
    {
        if (cController.GetComponent<AudioSource>())
        {
            AudioSource aSource = cController.GetComponent<AudioSource>();
            int fadeTime = 100;
            for (int i =0; i<fadeTime; i++)
            {
                aSource.volume -= aSource.volume / fadeTime;
            }
        }
    }
    public static void SetRaceTrackHandler(RaceTrackHandler rHandler)
    {
        trackHandler = rHandler;
    }

    public static void SetMeteorAdmin(MeteorAdmin _mAdmin)
    {
        mAdmin = _mAdmin;
    }

    public static void SetUIController(UIController _uIController)
    {
        uiController = _uIController;
    }

    public static void SetCarController(CarController _cController)
    {
        cController = _cController;
    }
}
