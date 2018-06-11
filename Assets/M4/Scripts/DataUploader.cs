using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class DataUploader : MonoBehaviour {

    public static DataUploader du;

    public string url= "http://daphne.rose.free.fr/test.php";
    public string url_password = "http://millenaire4.enpc.fr/game/authorization.php";
    void Awake () {
        if (du == null)
        {
            DontDestroyOnLoad(gameObject);
            du = this;
        }
        else if (du != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    public void Upload () {
        StartCoroutine(UploadData());
    }

    public IEnumerator UploadData() {
        print("datauploader.uploaddata lancé");
        print(url);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(DataControl.control.getJSON());
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        var ans = request.Send();
        
        print("LA REQUETE RESSEMBLE A CA: " + request);
        print("LA REPONSE RESSEMBLE A CA: " + ans);
        yield return ans;
        //yield return request.SendWebRequest();

        Debug.Log("Status Code: " + request.responseCode);
    }

    public IEnumerator checkConnectionData()
    {
        print("salutccccccc");
        var request = new UnityWebRequest(url_password, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(ConnectionDataControl.control.getLancementJeuJson());
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        var ans = request.Send();
        
        yield return ans;

        ConnectionDataControl.control.responseStr = request.downloadHandler.text;
        ConnectionDataControl.control.checkConnectionCode = (int)request.responseCode;
        print("valeurs recuperee: " + ConnectionDataControl.control.responseStr + " ||| " + ConnectionDataControl.control.checkConnectionCode);
        Debug.Log("Status Code: " + request.responseCode);
    }

    public IEnumerator checkLoginData(string target_url)
    {
        var request = new UnityWebRequest(target_url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(ConnectionDataControl.control.getLancementJeuJson());
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        var ans = request.Send();
        print("datauploader.checklogindata lancé");
        print("LA REQUETE RESSEMBLE A CA: " + request);
        print("LA REPONSE RESSEMBLE A CA: " + ans);
        yield return ans;

        ConnectionDataControl.control.responseStr = request.downloadHandler.text;
        ConnectionDataControl.control.checkConnectionCode = (int)request.responseCode;
        Debug.Log("Status Code: " + request.responseCode);
    }
}
