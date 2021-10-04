using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode{
    Efficiency,
    Timed,
    Zen,
    Puzzle
}

public class SessionInfo : MonoBehaviour
{
    public static SessionInfo Singleton = null;

    public static void SandwhichFinished(){
        if(Singleton) Singleton.sandwhichFinished += 1;
    }
    public static void IngredientWasted(){
        if(Singleton) Singleton.ingredientWasted += 1;
    }

    public GameMode gameMode = GameMode.Efficiency;
    public int sandwhichFinished = 0;
    public int ingredientWasted = 0;
    public int wasteLimit = 50;
    public float timeLimit = 120;

    public void ResetForTrial(){
        gameMode = GameMode.Efficiency;
        sandwhichFinished = 0;
        ingredientWasted = 0;
        wasteLimit = 50;
    }

    public void ResetForTimed(){
        gameMode = GameMode.Timed;
        sandwhichFinished = 0;
        ingredientWasted = 0;
        timeLimit = 120;
    }

    public void ResetForZen(){
        gameMode = GameMode.Zen;
        sandwhichFinished = 0;
        ingredientWasted = 0;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(Singleton){
            GameObject.Destroy(this.gameObject);
        } else {
            Singleton = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}
