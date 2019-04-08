using ARDesign.Serialize;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FinalizeHelper : MonoBehaviour {

    [SerializeField]
    private InputField jsonOut;

    [SerializeField]
    private InputField nameField;

    private string jsonToBuild;

    [SerializeField]
    private GameObject serverCommunication;
    [SerializeField]
    private GameObject webError;
    [SerializeField]
    private GameObject jsonBuild;
    [SerializeField]
    private GameObject success;
    [SerializeField]
    private Text successText;


    // Use this for initialization
    void Start() {
        jsonOut.text = VariableManager.instance.ToString();

        serverCommunication.SetActive(false);
        webError.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        jsonToBuild = jsonOut.text;
    }
    /* --Currently, server autosets ID -- this code all builds a custom ID based on count
    IEnumerator WebRequest(string json)
    {
        
        UnityWebRequest www = UnityWebRequest.Get(url + "/scenes/count");
        serverCommunication.SetActive(true);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            webError.SetActive(true);
        }
        else
        {
            string count = www.downloadHandler.text;
            yield return www.downloadHandler.text;
            serverCommunication.SetActive(false);
            jsonBuild.SetActive(true);

            int countNum = (int)JObject.Parse(count).SelectToken("count");
            //Debug.Log(countNum);

            int newIndex = countNum+1;
            //Debug.Log(newIndex);
            string jsonToPost = nameField.text == "" ? PrepPost(json, newIndex) : PrepPost(json, newIndex, nameField.text);
            //Debug.Log(jsonToPost);
            jsonBuild.SetActive(false);
            using (UnityWebRequest www2 = new UnityWebRequest(url + "/scenes"))
            {
                www2.method = UnityWebRequest.kHttpVerbPOST;
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonToPost);
                UploadHandler uploader = new UploadHandlerRaw(bytes);
                DownloadHandler downloader = new DownloadHandlerBuffer();
                uploader.contentType = "application/json";

                www2.uploadHandler = uploader;
                www2.downloadHandler = downloader;
                serverCommunication.SetActive(true);

                //Debug.Log(www2.GetRequestHeader());

                yield return www2.SendWebRequest();

                Debug.Log(www2.responseCode);
                if (www2.isNetworkError || www2.isHttpError)
                {
                    Debug.Log(www2.error);
                    webError.SetActive(true);
                    serverCommunication.SetActive(false);
                }
                else
                {
                    successText.text = "Success!! ID is " + newIndex;
                    success.SetActive(true);
                    serverCommunication.SetActive(false);
                }
            }
        }

    }
    */

    IEnumerator WebRequest(string json)
    {

        jsonBuild.SetActive(true);
        string jsonToPost = nameField.text == "" ? PrepPost(json) : PrepPost(json, nameField.text);
        jsonBuild.SetActive(false);
        using (UnityWebRequest www = new UnityWebRequest(ARDesign.Serialize.Utility.URL + "/scenes"))
        {
            www.method = UnityWebRequest.kHttpVerbPOST;
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonToPost);
            UploadHandler uploader = new UploadHandlerRaw(bytes);
            DownloadHandler downloader = new DownloadHandlerBuffer();
            uploader.contentType = "application/json";

            www.uploadHandler = uploader;
            www.downloadHandler = downloader;
            serverCommunication.SetActive(true);

            //Debug.Log(www2.GetRequestHeader());

            yield return www.SendWebRequest();

            Debug.Log(www.responseCode);
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                webError.SetActive(true);
                serverCommunication.SetActive(false);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                yield return www.downloadHandler.text;
                Debug.Log(jsonResponse);
                string id = (string)JObject.Parse(jsonResponse).SelectToken("id");
                Debug.Log(id);
                successText.text = string.Concat("Success!! ID is ", id);
                success.SetActive(true);
                serverCommunication.SetActive(false);
            }
        }
    }

    private static string PrepPost(string json, string name_)//int id_)
    {
        PostBody toReturn = new PostBody
        {
            name = name_,
            //id = id_,
            config = JObject.Parse(json)
        };
        return JsonConvert.SerializeObject(toReturn);
    }
    private static string PrepPost(string json)// int id_)
    {
        PostBodyNoName toReturn = new PostBodyNoName
        {
            //id = id_,
            config = JObject.Parse(json)
        };
        return JsonConvert.SerializeObject(toReturn);
    }

    public void SubmitJSON()
    {
        success.SetActive(false);
        serverCommunication.SetActive(false);
        webError.SetActive(false);
        jsonBuild.SetActive(false);
        StartCoroutine(WebRequest(jsonToBuild));
    }

    private class PostBody{
        //public int id;
        public string name = null;
        public JObject config;
    }
    private class PostBodyNoName
    {
        //public int id;
        public JObject config;
    }

}
