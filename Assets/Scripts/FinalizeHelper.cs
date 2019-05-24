using VRTour.Serialize;
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

    IEnumerator WebRequest(string json)
    {

        using (UnityWebRequest www = Publisher.SendTour(nameField.text, json))
        {
            serverCommunication.SetActive(true);

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

    
    public void SubmitJSON()
    {
        success.SetActive(false);
        serverCommunication.SetActive(false);
        webError.SetActive(false);
        StartCoroutine(WebRequest(jsonToBuild));
    }

    

}
