using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public GameObject helpPanel;

    public void StartGame_Trial()
    {
        SessionInfo.Singleton.ResetForTrial();
        SceneManager.LoadScene(1);
    }

    public void StartGame_Timed()
    {
        SessionInfo.Singleton.ResetForTimed();
        SceneManager.LoadScene(1);
    }

    public void StartGame_Zen()
    {
        SessionInfo.Singleton.ResetForZen();
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
