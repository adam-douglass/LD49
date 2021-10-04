using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrialLogic : MonoBehaviour
{
    public Text text;
    private SessionInfo session;

    // Start is called before the first frame update
    void Start()
    {
        session = SessionInfo.Singleton;
    }

    // Update is called once per frame
    void Update()
    {
        int remaining = session.wasteLimit - session.ingredientWasted;
        text.text = remaining.ToString();
        if(remaining <= 0){
            SceneManager.LoadScene("Finish");
        }
    }
}
