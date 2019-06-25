using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI text;

    public enum TimerType
    {
        RunTimer, InitialTimer
    };
    [SerializeField] TimerType tType;


    private float timer;
    private bool canCount = false;
    private bool doOnce = false;
    private bool colorIsRed = true;

    private void Awake()
    {
        switch (tType)
        {
            case TimerType.InitialTimer:                //Le mando el timer inicial al level manager para 
                LevelManager.SetInitialTimer(this);
                timer = 3f;
                break;
            case TimerType.RunTimer:
                timer = LevelManager.GetMainTimerTime();
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = timer.ToString() + ".00";
        CheckTimerColor();
    }

    private void Update()                   // Timer debe ser o inicial o de carrera. 
    {
        switch (tType)
        {
            case TimerType.InitialTimer:
                UpdateInitialTimer();
                break;
            case TimerType.RunTimer:
                UpdateRunTimer();
                break;
            default:
                break;
        }
    }

    void UpdateInitialTimer()
    {
        if(timer>= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            text.text = timer.ToString("F");
        }
        else if (timer <= 0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            text.text = "0.00";
            timer = 0.0f;
            StartGame();
        }
    }

    void UpdateRunTimer()
    {
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            text.text = timer.ToString("F");
            CheckTimerColor();
        }
        else if (timer <= 0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            text.text = "0.00";
            timer = 0.0f;
            GameOver();
        }
    }

    void CheckTimerColor()
    {
        if (timer <= 10f)
        {
            if (!colorIsRed)
            {
                colorIsRed = true;
                TextColorDisplay.SetTextColourRed(text);
            }
        }
        else
        {
            if(colorIsRed)
            {
                TextColorDisplay.SetWordColourBlue(text);
                colorIsRed = false;
            }
        }
    }

    void StartGame()
    {
        LevelManager.StartGame();
    }

    void GameOver()
    {
        LevelManager.YouLoose();
    }

    public void AddToTimer(float deltaTime)
    {
        timer += deltaTime;
    }

    public void SetTimer(float incomingTime)
    {
        timer = incomingTime;
    }

    public void SetCanCount(bool _canCount)
    {
        canCount = _canCount;

    }


}
