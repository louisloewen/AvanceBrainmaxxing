using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Data
{
    public int value;
}


public class Confidence : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    private void Start()
    {
        Debug.Log("Button 1: " + (button1 != null ? "Assigned" : "Not Assigned"));
        Debug.Log("Button 2: " + (button2 != null ? "Assigned" : "Not Assigned"));
        Debug.Log("Button 3: " + (button3 != null ? "Assigned" : "Not Assigned"));


        button1.onClick.AddListener(() => OnButtonClicked(1));
        button2.onClick.AddListener(() => OnButtonClicked(2));
        button3.onClick.AddListener(() => OnButtonClicked(3));

    }

    public void OnButtonClicked(int value)
    {
        StartCoroutine(SendDataToAPI(value));
    }

    private IEnumerator SendDataToAPI(int value)
    {
        string url = "http://your-api-url/api/endpoint"; 

        // Create a data object
        Data data = new Data { value = value };

        // Convert data object to JSON
        string jsonData = JsonUtility.ToJson(data);

        // Create UnityWebRequest
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Success: " + request.downloadHandler.text);
        }
    }
}
