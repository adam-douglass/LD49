using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject tipPanel;
    public Text tipText;

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

    public void StartGame_Puzzle()
    {
        SessionInfo.Singleton.ResetForPuzzle();
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

    public void OpenTip(string tip)
    {
        tipText.text = tip;
        tipPanel.SetActive(true);
    }
    public void CloseTip()
    {
        tipPanel.SetActive(false);
    }

    public void Leave()
    {
        Application.Quit();
    }
}
