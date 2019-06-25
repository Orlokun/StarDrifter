using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class Register : MonoBehaviour
{
    public GameObject userNameObject;
    public GameObject eMailObj;
    public GameObject passwordObj;
    public GameObject confPasswordObj;
    public GameObject pathTextBox;
    private InputField pathFile;
    private InputField[] iFields;
    private string userName;
    private string eMail;
    private string password;
    private string confPassword;
    private string path;
    private string form;
    private bool emailValid = false;
    private string[] myCharacters = {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","ñ","o","p",
                                     "q","r","s","t","u","v","w","x","y","z","A","B","C","D","E","F",
                                     "G","H","I","J","K","L","M","N","Ñ","0","P","Q","R","S","T","U","V",
                                     "W","X","Y","Z","0","1","2","3","4","5","6","7","8","9"};

    void Start()
    {
        RegisterInputFields();
    }

    void RegisterInputFields()
    {
        pathFile = pathTextBox.GetComponent<InputField>();
        iFields = new InputField[4];                         //el número es el número de objetos que se usan de la escena
        for (int i = 0; i < iFields.Length; i++)
        {
            switch (i)
            {
                case 0:
                    iFields[i] = userNameObject.GetComponent<InputField>();
                    break;
                case 1:
                    iFields[i] = eMailObj.GetComponent<InputField>();
                    break;
                case 2:
                    iFields[i] = passwordObj.GetComponent<InputField>();
                    break;
                case 3:
                    iFields[i] = confPasswordObj.GetComponent<InputField>();
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeTextFocus();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            RegisterButton();
        }
        SetInputValues();
    }

    void SetInputValues()
    {
        for (int i = 0; i < iFields.Length; i++)
        {
            switch (i)
            {
                case 0:
                    userName = iFields[i].text;
                    break;
                case 1:
                    eMail = iFields[i].text;
                    break;
                case 2:
                    password = iFields[i].text;
                    break;
                case 3:
                    confPassword = iFields[i].text;
                    break;
            }
        }
        path = pathFile.text;
    }

    public void RegisterButton()
    {
        if (userName != "" && eMail != "" && password != "" && confPassword != "")
        {
            CheckInputFields();
        }
    }

    void CheckInputFields()
    {
        bool uName = false;
        bool uEmail = false;
        bool uPassword = false;
        bool confirmPw = false;

        uName = CheckUNameUsed(userName);
        EmailValidation();
        uEmail = CheckEmail(eMail);
        uPassword = CheckPassword(password);
        if (uPassword)
        {
            confirmPw = CheckConfirmationPassword(confPassword);
        }
        CheckResult(uName, uEmail, uPassword, confirmPw);
        ClearMyValues();
    }

    void CheckResult(bool _uName, bool _uMail, bool _uPassword, bool _confPS)
    {
        if (_uMail && _uMail && _uPassword && _confPS)
        {
            //EncryptPassword();
            form = (userName + Environment.NewLine + eMail + Environment.NewLine + password);
            CreateFile(form);
            ClearAllFields();
            Debug.Log("Registration Complete");
        }
        else
        {
            Debug.LogError("Oops, parece que faltó algún dato");
        }
    }

    void CreateFile(string _form)
    {
        System.IO.File.WriteAllText(GetPath() + "/StarDrifterLog/" + userName + ".txt", form);
    }

    string GetPath()
    {
        if (pathFile.text == "")
        {
            Debug.LogError("You must set a Path for the 'StarDrifter' folder");
            return "";
        }
        else
        {
            return path;
        }
    }

    void ClearAllFields()
    {
        for (int i = 0; i < iFields.Length; i++)
        {
            iFields[i].text = "";
        }
    }

    void ClearMyValues()
    {
        userName = "";
        eMail = "";
        password = "";
        confPassword = "";
        path = "";
    }

    void EncryptPassword()
    {
        bool clear = true;
        int i = 1;
        foreach (char c in password)
        {
            if (clear)
            {
                password = "";
                char cryptedChar = (char)(c * i);
                password += cryptedChar.ToString();
                i++;
            }
        }
    }
    bool CheckConfirmationPassword(string _confPassword)
    {
        if (_confPassword == "")
        {
            Debug.LogError("You must Insert a Confirmation Password");
        }
        if (confPassword == password)
        {
            return true;
        }
        else
        {
            Debug.LogError("Confirmation password does not match");
            return false;
        }
    }

    bool CheckPassword(string _password)
    {
        if (_password == "")
        {
            Debug.LogError("Password is Empty");
            return false;
        }

        if (_password.Length > 5)
        {
            return true;
        }
        else
        {
            Debug.LogError("Password must be at least 6 characters long");
            return false;
        }
    }

    bool CheckUNameUsed(string _uName)
    {
        if (_uName == "")
        {
            Debug.LogError("No username input");
            return false;
        }
        if (!System.IO.File.Exists(@"E:/UnityTestFolder/" + userName + ".txt"))
        {
            return true;
        }
        else
        {
            Debug.LogError("Username alreadyExist");
            return false;
        }
    }

    bool CheckEmail(string _eMail)
    {
        if (emailValid)
        {
            if (_eMail == "")
            {
                Debug.LogError("No email input");
                return false;
            }
            else
            {
                if (_eMail.Contains("@"))
                {
                    if (_eMail.Contains("."))
                    {
                        return true;
                    }
                    else
                    {
                        Debug.LogError("Email must contain a dot: .");
                        return false;
                    }
                }
                else
                {
                    Debug.LogError("Email must contain an @");
                    return false;
                }
            }
        }
        else
        {
            Debug.LogError("Email is not valid");
            return false;
        }
    }

    void EmailValidation()
    {
        bool startsWith = false;
        bool endsWith = false;

        for (int i = 0; i < myCharacters.Length; i++)
        {
            if (eMail.StartsWith(myCharacters[i]))
            {
                startsWith = true;
            }
            if (eMail.EndsWith(myCharacters[i]))
            {
                endsWith = true;
            }
        }
        if (endsWith && startsWith)
        {
            emailValid = true;
        }
        else
        {
            emailValid = false;
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
}
