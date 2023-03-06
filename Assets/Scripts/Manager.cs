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

        //�摜���擾�ł���܂ő҂�
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //�擾�����摜�̃e�N�X�`����RawImage�̃e�N�X�`���ɒ���t����
            _image.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            _image.SetNativeSize();
        }
    }
}
