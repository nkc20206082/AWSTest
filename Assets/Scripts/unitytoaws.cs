using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
public class unitytoaws : MonoBehaviour
{
    private string ID;//欲しい情報のID
    [SerializeField] private DynamoResponse response;//返ってきたものを表示する
    [SerializeField] private GameObject Rawimage;
    [SerializeField] private GameObject TryButton;
    [SerializeField] private GameObject RetryButton;
    [SerializeField] Manager Manager;

    public string URL;//URLを入れるよう

    [Serializable]
    public class DynamoIftest
    {
        public DynamoQueryKey Keys;
        [Serializable]
        public class DynamoQueryKey
        {
            public string ID;//クエリに使うキー
        }
    }

    [Serializable]
    public class DynamoResponse
    {
        public List<DynamoResponseItems> body;
        [Serializable]
        public class DynamoResponseItems
        {
            public string ID;//取得ID
            public string URL;//取得URL
        }
    }

    [Obsolete]
    public void Startacces()
    {
        ID = UnityEngine.Random.Range(1,8).ToString();
        StartCoroutine(AwsApiTest());
    }

    [Obsolete]
    private IEnumerator AwsApiTest()
    {
        var awsElement = new DynamoIftest()
        {
            Keys = new DynamoIftest.DynamoQueryKey() { ID = ID}
        };
        string jsonStr = JsonUtility.ToJson(awsElement);

        var request = new UnityWebRequest();

        request.url = "APIゲートウェイのURL";//APIゲートウェイとの通信
        var body = Encoding.UTF8.GetBytes(jsonStr);
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.method = UnityWebRequest.kHttpVerbPOST;
        yield return request.Send();

        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                TryButton.SetActive(false);
                Rawimage.SetActive(true);
                RetryButton.SetActive(true);
                var responseJsonText = request.downloadHandler.text;
                Debug.Log(responseJsonText);
                response = JsonUtility.FromJson<DynamoResponse>(responseJsonText);
                Debug.Log(response.body[0].URL);
                URL = response.body[0].URL;

                Manager.URLs.Clear();
                foreach (var item in response.body)
                {
                    Manager.URLs.Add(item.URL);
                }
                Manager.StartCoroutine("Indicate");
            }
            else
            {
                Debug.Log("failed");
            }
        }
    }
}
