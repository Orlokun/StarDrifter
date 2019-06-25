using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class Login : MonoBehaviour
{
    MenuManager mManager;
    public GameObject userNameObj;
    public GameObject passwordObj;
    public GameObject pathObj;

    private InputField pathInput;
    private InputField[] iFields;

    private string path;
    private string userName;
    private string password;

    private string[] fileLines;

    void Start()
    {
        SetInputFields();
        mManager = FindObjectOfType<MenuManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeTextFocus();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (InputFieldsHaveCharacters())
            {
                LogInButton();
            }
        }
        userName = iFields[0].text;                         //Esto está muy hardcodeado, hay que arreglarlo.
        password = iFields[1].text;
        path = pathInput.text;
    }

    public void LogInButton()
    {
        bool _uName = false;
        bool _uPass = false;

        _uName = GetUName();
        if (_uName)
        {
            ReadLines();
        }
        _uPass = GetUPass();

        if (_uName && _uPass)
        {
            ClearValues();
            mManager.MenuSecondSection();
            Debug.Log("Log in Successfull");
        }
    }

    private void ClearValues()
    {
        userName = "";
        password = "";  
    }

    private bool GetUName()
    {
        if (System.IO.File.Exists(GetPath() + "/StarDrifterLog/" + userName + ".txt"))
        {
            return true;
        }
        else
        {
            Debug.LogError("The User Doesn't Exist");
            return false;
        }
    }

    void ReadLines()
    {
        fileLines = System.IO.File.ReadAllLines(GetPath() + "/StarDrifterLog/" + userName + ".txt");
    }

    bool GetUPass()
    {
        if (System.IO.File.Exists(GetPath() + "/StarDrifterLog/" + userName + ".txt"))
        {
            if (password.Length > 5)
            {
                if (password == fileLines[2])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Debug.LogError("Password must be at least 6 characters long");
                return false;
            }
        }
        return false;
    }

    bool InputFieldsHaveCharacters()
    {
        bool _user = false;
        bool _pass = false;
        for (int i = 0; i < iFields.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (userName == "")
                    {
                        _user = false;
                        Debug.LogError("User name is Empty");
                    }
                    else
                    {
                        _user = true;
                    }
                    break;
                case 1:
                    if (password == "")
                    {
                        _pass = false;
                        Debug.LogError("Password is Empty");
                    }
                    else
                    {
                        _pass = true;
                    }
                    break;
                default:
                    break;
            }
        }
        if (_user && _pass)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetInputFields()
    {
        pathInput = pathObj.GetComponent<InputField>();
        path = pathInput.text;
        iFields = new InputField[2];                         //el número es el número de objetos que se usan de la escena
        for (int i = 0; i < iFields.Length; i++)
        {
            switch (i)
            {
                case 0:
                    iFields[i] = userNameObj.GetComponent<InputField>();
                    break;
                case 1:
                    iFields[i] = passwordObj.GetComponent<InputField>();
                    break;
                default:
                    break;
            }
        }
    }

    void ChangeTextFocus()
    {
        for (int i = 0; i < iFields.Length; i++)
        {
            InputField iField = iFields[i];
            if (iField.isFocused)
            {
                i++;
                if (i >= iFields.Length)
                {
                    i = 0;
                }
                iFields[i].Select();
                break;
            }
        }
    }

    string GetPath()
    {
        if (pathInput.text == "")
        {
            Debug.LogError("You must set a Path for the 'StarDrifterLog' folder");
            return "";
        }
        else
        {
            return path;
        }
    }
}
