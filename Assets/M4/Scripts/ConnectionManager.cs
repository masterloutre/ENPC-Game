using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ConnectionDataControl.control.phasePassword = phasePassword;
        print(ConnectionDataControl.control.phasePassword);
        bool isPassword = ConnectionDataControl.control.checkPhasePassword();
        lancement_jeu lancementjeu = new lancement_jeu();
        lancementjeu.mdp = phasePassword;
        ConnectionDataControl.control.lancementjeu = lancementjeu;
        ConnectionDataControl.control.SaveConnection();
        isPassword = true;
        if (isPassword)
        {
            //ConnectionDataControl.control.SaveConnection();
            SceneLoadEvents.sceneOnLoad.UpdateScene("LogIn");
        }
        else
        {
            displayErrorText();
        }
        //inclure un message d'erreur de connexion lorsque la connexion au serveur échoue
    }

    public void displayErrorText()
    {
        print("erreur, le mot de passe est erroné");
        Color color = phaseErrorMessageGO.GetComponentInChildren<UnityEngine.UI.Text>().color;
        color.a = 1;
        phaseErrorMessageGO.GetComponentInChildren<UnityEngine.UI.Text>().color = color;
    }

    public void sendLoginInfo()
    {
        string playerLogin = playerLoginGO.GetComponentInChildren<UnityEngine.UI.Text>().text;
        string playerPassword = playerPasswordGO.GetComponentInChildren<UnityEngine.UI.Text>().text;
        bool isPassword = true;
        if (isPassword)
        {
            SceneLoadEvents.sceneOnLoad.UpdateScene("Default_scene");
        }
        else
        {
            displayErrorText();
        }
    }
}
