using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Manager : MonoBehaviour
{
    public List<string> URLs = new List<string>();
    [SerializeField] private RawImage _image;

    [System.Obsolete]
    public IEnumerator  Indicate()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(URLs[0]);

        //画像を取得できるまで待つ
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //取得した画像のテクスチャをRawImageのテクスチャに張り付ける
            _image.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            _image.SetNativeSize();
        }
    }
}
