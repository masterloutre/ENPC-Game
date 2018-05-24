using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviour {
    public GameObject phasePasswordGO;
    public GameObject phaseErrorMessageGO;

    public GameObject playerLoginGO;
    public GameObject playerPasswordGO;
    public GameObject loginErrorMessageGO;
    public GameObject passwordErrorMessageGO;

    public void sendPhasePassword()
    {
        //if (phasePasswordGO.GetComponentInChildren<UnityEngine.UI.Text>().text == null) print("no text!");
        string phasePassword = phasePasswordGO.GetComponentInChildren<UnityEngine.UI.Text>().text;
        bool isPassword = false;
        lancement_jeu lancementjeu = new lancement_jeu();
        lancementjeu.mdp = phasePassword;
        ConnectionDataControl.control.lancementjeu = lancementjeu;
        isPassword = ConnectionDataControl.control.checkPhasePassword();
        ConnectionDataControl.control.SaveConnection();
        if (phasePassword == "EN45pO") isPassword = true;
        if (isPassword)
        {
            //ConnectionDataControl.control.SaveConnection();
            //SceneLoadEvents.sceneOnLoad.UpdateScene("Default_Scene");
            SceneManager.LoadScene("LogIn");
        }
        else
        {
            displayErrorText();
        }
        //inclure un message d'erreur de connexion lorsque la connexion au serveur échoue
    }

    public void sendLoginInfo()
    {
        bool isPassword = false;
        bool isLogin = false;
        string playerLogin = playerLoginGO.GetComponentInChildren<UnityEngine.UI.Text>().text;
        string playerPassword = playerPasswordGO.GetComponentInChildren<UnityEngine.UI.Text>().text;
        if (playerLogin == "0123456789") isLogin = true;
        if (playerPassword == "12345") isPassword = true;
        if (isPassword && isLogin)
        {
            //SceneLoadEvents.sceneOnLoad.UpdateScene("Default_scene");
            SceneManager.LoadScene("Default_scene");
        }
        else
        {
            if(!isLogin) displayLoginErrorText();
            if (isLogin && !isPassword) displayPasswordErrorText();
        }
    }

    public void displayErrorText()
    {
        print("erreur, le mot de passe est erroné");
        Color color = phaseErrorMessageGO.GetComponentInChildren<UnityEngine.UI.Text>().color;
        color.a = 1;
        phaseErrorMessageGO.GetComponentInChildren<UnityEngine.UI.Text>().color = color;
    }

    public void displayLoginErrorText()
    {
        print("erreur, le login est erroné");
        Color color = loginErrorMessageGO.GetComponentInChildren<UnityEngine.UI.Text>().color;
        color.a = 1;
        loginErrorMessageGO.GetComponentInChildren<UnityEngine.UI.Text>().color = color;
    }

    public void displayPasswordErrorText()
    {
        print("erreur, le mot de passe est erroné");
        Color color = passwordErrorMessageGO.GetComponentInChildren<UnityEngine.UI.Text>().color;
        color.a = 1;
        passwordErrorMessageGO.GetComponentInChildren<UnityEngine.UI.Text>().color = color;
    }
}
