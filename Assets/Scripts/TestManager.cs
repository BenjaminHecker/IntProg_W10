using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestManager : MonoBehaviour
{
    private void Start()
    {
        //string url = "https://jsonplaceholder.typicode.com/todos/1";
        string url = "https://jsonplaceholder.typicode.com/comments";

        StartCoroutine(GetJsonFromUrl(url, Receiver2));
    }

    private IEnumerator GetJsonFromUrl(string url, System.Action<string> callback)
    {
        string jsonText;

        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            jsonText = www.error;
        }
        else
        {
            jsonText = www.downloadHandler.text;
        }

        www.Dispose();

        callback(jsonText);
    }

    private void Receiver1(string jsonText)
    {
        JsonReceiver1 receiver = JsonUtility.FromJson<JsonReceiver1>(jsonText);

        Debug.Log(receiver.userId);
        Debug.Log(receiver.id);
        Debug.Log(receiver.title);
        Debug.Log(receiver.completed);
    }

    private void Receiver2(string jsonText)
    {
        JsonReceiver2 receiver = JsonUtility.FromJson<JsonReceiver2>("{\"comments\":" +  jsonText + "}");
        Comment[] comments = receiver.comments;

        foreach(Comment comment in comments)
        {
            Debug.Log(comment.email + " : " + comment.body);
        }
    }
}
