using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    [SerializeField] private Transform canvas;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SwitchMenu(string names)
    {
        var _names = names.Split(' ');
        canvas.Find(_names[0]).gameObject.SetActive(false);
        canvas.Find(_names[1]).gameObject.SetActive(true);
    }
}