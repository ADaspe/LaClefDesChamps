using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void LoadBootScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

    /*public void Test()
    {
        Debug.Log("Bouton cliqué");
    }*/
}
