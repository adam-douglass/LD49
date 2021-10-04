using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishMenu : MonoBehaviour
{
    public Text counter;
    public Text total;

    public void Return()
    {
        SceneManager.LoadScene(0);
    }

    public void Start(){
        var session = SessionInfo.Singleton;
        if(session){
            total.text = session.sandwhichFinished.ToString();
            int remaining = Mathf.CeilToInt(session.duration);
            int minutes = remaining/60;
            int seconds = remaining%60;
            counter.text = $"{minutes}:{seconds:00}";
        }
    }
}
