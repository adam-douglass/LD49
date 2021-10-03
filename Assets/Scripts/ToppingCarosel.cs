using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingCarosel : MonoBehaviour
{

    float generationArea = -16;
    public GameObject FactoryClass;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float left = -generationArea;
        foreach(var obj in this.gameObject.GetComponentsInChildren<ToppingFactory>()){
            if(obj.transform.position.x  > -generationArea){
                GameObject.Destroy(obj.gameObject);
            }
            left = Mathf.Min(obj.transform.position.x, left);
        }
        if(left > generationArea){
            var position = new Vector3(generationArea - 2, this.transform.position.y, 0);
            var created = Instantiate(FactoryClass, position, Quaternion.identity);
            created.transform.SetParent(this.transform);
        }
    }
}
