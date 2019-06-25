using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject loginMenu;
    public GameObject registerMenu;
    public GameObject pathObj;

    public GameObject diffObj;
    public GameObject timeObj;

    InputField diffInput;
    InputField timeInput;

    public GameObject startGameButton;

    private void Start()
    {
        SetObjects();
        InitialMenu();
    }

    void SetObjects()
    {
        diffInput = diffObj.GetComponent<InputField>();
        timeInput = timeObj.GetComponent<InputField>();
    }

    void InitialMenu()
    {
        startGameButton.SetActive(false);
        diffObj.SetActive(false);
        timeObj.SetActive(false);
    }

    public void MenuSecondSection()
    {
        loginMenu.SetActive(false);
        registerMenu.SetActive(false);
        pathObj.SetActive(false);

        startGameButton.SetActive(true);
        diffObj.SetActive(true);
        timeObj.SetActive(true);
    }


    public void SetGameParameters()
    {
        int diff = int.Parse(diffInput.text);
        int timeAmount = int.Parse(timeInput.text);

        if (diff > 2)
        {
            diff = 2;
        }
        LevelManager.SetDifficulty(diff);
        LevelManager.SetInitialTimeAmount(timeAmount);
    }

    public void StartGameButton()
    {
        if (CheckData())
        {
            SetGameParameters();
            StartGame();
        }
    }

    private void StartGame()
    {
        Scene actualScene = SceneManager.GetActiveScene();
        int sceneIndex = actualScene.buildIndex;
        LevelManager.LoadNextScene(sceneIndex);
    }

    private bool CheckData()
    {
        if (diffInput.text != "" && timeInput.text != "")
        {
            int _diff;
            int _time;
            if (int.TryParse(diffInput.text, out _diff) && int.TryParse(timeInput.text, out _time))
            {
                return true;
            }
            Debug.LogError("Difficulty or Time is not an INT");
            return false;
        }
        else
        {
            Debug.LogError("Difficulty Level or TimeSet is Empty");
            return false;
        }
    }

}
