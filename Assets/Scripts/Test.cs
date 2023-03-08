using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
public class Test : MonoBehaviour
{

    [Obsolete]
    void Start()
    {
        StartCoroutine(AwsApiTest2());
    }

    [Obsolete]
    private IEnumerator AwsApiTest2()
    {
        var request = new UnityWebRequest();

        request.url = "https://pmflwfxxy3.execute-api.ap-northeast-1.amazonaws.com/prod2";//APIゲートウェイとの通信
        yield return request.Send();

        Debug.Log(request.responseCode);
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                Debug.Log("true");
            }
            else
            {
                Debug.Log("failed");
            }
        }
    }
}
