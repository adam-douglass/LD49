using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingCarosel : MonoBehaviour
{

    float generationArea = -16;
    public GameObject FactoryClass;

    private Randwhich rand;
    // Start is called before the first frame update
    void Start()
    {
        rand = new Randwhich();
    }

    public Randwhich Rand
    {
        get
        {
            return rand;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float left = -generationArea;
        foreach(var obj in this.gameObject.GetComponentsInChildren<ToppingFactory>()){
            if(obj.transform.position.x  > -generationArea){
                GameObject.Destroy(obj.gameObject);
            }
            var topping = obj.gameObject.GetComponentInChildren<Topping>();
            if(topping){
                left = Mathf.Min(obj.transform.position.x - topping.Width/2.0f, left);
            }
            
        }
        Debug.Log(left);
        if(left > generationArea){
            var position = new Vector3(generationArea, this.transform.position.y, 0);
            var created = Instantiate(FactoryClass, position, Quaternion.identity);
            created.transform.SetParent(this.transform);
        }
    }
}
