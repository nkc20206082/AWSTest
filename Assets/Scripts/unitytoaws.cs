using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
public class unitytoaws : MonoBehaviour
{
    [SerializeField] private string ID;//�~��������ID
    [SerializeField] private DynamoResponse response;//�Ԃ��Ă������̂�\������
    [SerializeField] Manager Manager;

    public string URL;//URL������悤

    [Serializable]
    public class DynamoIftest
    {
        public DynamoQueryKey Keys;
        [Serializable]
        public class DynamoQueryKey
        {
            public string ID;//�N�G���Ɏg���L�[
        }
    }

    [Serializable]
    public class DynamoResponse
    {
        public List<DynamoResponseItems> body;
        [Serializable]
        public class DynamoResponseItems
        {
            public string ID;//�擾ID
            public string URL;//�擾URL
        }
    }

    [Obsolete]
    void Start()
    {
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

        request.url = "https://ahcjrkat0j.execute-api.ap-northeast-1.amazonaws.com/prod";//API�Q�[�g�E�F�C�Ƃ̒ʐM
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
