using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private unitytoaws unitytoaws;
    public void ButtonPush()
    {
        unitytoaws.Startacces();
    }

    public void retry()
    {
        SceneManager.LoadScene(0);
    }
}
