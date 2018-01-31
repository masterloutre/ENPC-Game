using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class DataUploader : MonoBehaviour {

    public static DataUploader du;

    //public string url= "http://daphne.rose.free.fr/test.php";
    public string url = "testurl";
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

        print(url);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(DataControl.control.getJSON());
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log("Status Code: " + request.responseCode);
    }

    public IEnumerator checkConnexionData()
    {

        print(url);
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(ConnectionDataControl.control.getPasswordJSON());
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log("Status Code: " + request.responseCode);
    }
}
