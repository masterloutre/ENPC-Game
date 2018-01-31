using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviour {
    public GameObject phasePasswordGO;
    public GameObject errorMessageGO;

    public void sendPhasePassword()
    {
        if (phasePasswordGO.GetComponentInChildren<UnityEngine.UI.Text>().text == null) print("no text!");
        string phasePassword = phasePasswordGO.GetComponentInChildren<UnityEngine.UI.Text>().text;
        ConnectionDataControl.control.phasePassword = phasePassword;
        print(ConnectionDataControl.control.phasePassword);
        bool isPassword = ConnectionDataControl.control.checkPhasePassword();
        if (isPassword)
        {
            SceneLoadEvents.sceneOnLoad.UpdateScene("Default_scene");
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
        Color color = errorMessageGO.GetComponentInChildren<UnityEngine.UI.Text>().color;
        color.a = 1;
        errorMessageGO.GetComponentInChildren<UnityEngine.UI.Text>().color = color;
    }
}
