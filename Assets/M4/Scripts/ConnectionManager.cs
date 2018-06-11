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

        //récupère le text dans le component input de la scène
        string phasePassword = phasePasswordGO.GetComponentInChildren<UnityEngine.UI.Text>().text;
        bool isPassword = false;

        //information pour constituer le header de requête
        lancement_jeu lancementjeu = new lancement_jeu();
        lancementjeu.mdp = phasePassword;
        ConnectionDataControl.control.lancementjeu = lancementjeu;

        // Vérification : appel au serveur
        isPassword = ConnectionDataControl.control.checkPhasePassword();

        // ???
        ConnectionDataControl.control.SaveConnection();

        //Final check : réponse du serveur positive ?
        if (phasePassword == "EN45pO") isPassword = true; // accès par défaut en placeholder
        if (isPassword)
        {
            //ConnectionDataControl.control.SaveConnection();
            //SceneLoadEvents.sceneOnLoad.UpdateScene("Default_Scene");

            //on passe à la scène suivante
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

        //Récupération des données en input de unity
        string playerLogin = playerLoginGO.GetComponentInChildren<UnityEngine.UI.Text>().text;
        string playerPassword = playerPasswordGO.GetComponentInChildren<UnityEngine.UI.Text>().text;

        // Vérification serveur
        // Final check
        if (playerLogin == "0123456789") isLogin = true;
        if (playerPassword == "12345") isPassword = true;
        if (isPassword && isLogin)
        {
            //SceneLoadEvents.sceneOnLoad.UpdateScene("Default_scene");

            // scène suivante
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
