using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public GameObject helpPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Help()
    {
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }

    public void Leave()
    {
        Application.Quit();
    }
}
