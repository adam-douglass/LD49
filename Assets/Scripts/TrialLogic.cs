using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrialLogic : MonoBehaviour
{
    public GameObject EfficiencyUI;
    public GameObject TimedUI;
    public GameObject ZenUI;

    private float start;
    private Text text;
    private SessionInfo session;

    // Start is called before the first frame update
    void Start()
    {
        start = Time.time;
        session = SessionInfo.Singleton;
        EfficiencyUI.SetActive(false);
        TimedUI.SetActive(false);
        ZenUI.SetActive(false);

        switch(session.gameMode){
            case GameMode.Efficiency:
                EfficiencyUI.SetActive(true);
                text = EfficiencyUI.transform.Find("Counter").GetComponent<Text>();
                break;
            case GameMode.Timed:
                TimedUI.SetActive(true);
                text = TimedUI.transform.Find("Counter").GetComponent<Text>();
                break;
            case GameMode.Zen:
                ZenUI.SetActive(true);
                // text = EfficiencyUI.transform.Find("Counter");
                break;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        switch(session.gameMode){
            case GameMode.Efficiency: {
                int remaining = session.wasteLimit - session.ingredientWasted;
                text.text = remaining.ToString();
                if(remaining <= 0){
                    SceneManager.LoadScene("Finish");
                }
            } break;
            case GameMode.Timed: {
                int remaining = Mathf.CeilToInt(session.timeLimit - (Time.time - start));
                int minutes = remaining/60;
                int seconds = remaining%60;
                text.text = $"{minutes}:{seconds:00}";
                if(remaining <= 0){
                    SceneManager.LoadScene("Finish");
                } 
            } break;
            case GameMode.Zen: {
                // int remaining = session.wasteLimit - session.ingredientWasted;
                // text.text = remaining.ToString();
                // if(remaining <= 0){
                //     SceneManager.LoadScene("Finish");
                // } 
            } break;
        }
    }

    public void Finished(){
        SceneManager.LoadScene("Finish");
    }
}
