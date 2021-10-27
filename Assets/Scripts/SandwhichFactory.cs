using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwhichFactory : MonoBehaviour
{
    public GameObject Sandwhich;
    float generationArea = 19;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float right = 0;
        foreach(var obj in this.gameObject.GetComponentsInChildren<SandwhichSled>()){
            // if(obj.transform.position.x < -generationArea){
            //     GameObject.Destroy(obj.gameObject);
            // }
            right = Mathf.Max(obj.CalculateBounds().max.x, right);
        }
        if(right < generationArea - 4){
            var position = new Vector3(generationArea, this.transform.position.y, 0);
            var created = Instantiate(Sandwhich, position, Quaternion.identity);
            created.transform.SetParent(this.transform);
        }
    }
}
