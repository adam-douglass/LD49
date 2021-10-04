using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionInfo : MonoBehaviour
{
    public static SessionInfo Singleton = null;

    public static void SandwhichFinished(){
        if(Singleton) Singleton.sandwhichFinished += 1;
    }
    public static void IngredientWasted(){
        if(Singleton) Singleton.ingredientWasted += 1;
    }

    public int sandwhichFinished = 0;
    public int ingredientWasted = 0;

    public void Reset(){
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
