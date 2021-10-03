using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwhichFactory : MonoBehaviour
{
    public GameObject Sandwhich;
    float generationArea = 0;

    // Start is called before the first frame update
    void Start()
    {
        generationArea = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }

    // Update is called once per frame
    void Update()
    {
        float right = 0;
        foreach(var obj in this.gameObject.GetComponentsInChildren<SandwhichSled>()){
            if(obj.transform.position.x < -generationArea){
                GameObject.Destroy(obj.gameObject);
            }
            right = Mathf.Max(obj.transform.position.x, right);
        }
        if(right < generationArea){
            var position = new Vector3(generationArea + 10, this.transform.position.y, 0);
            var created = Instantiate(Sandwhich, position, Quaternion.identity);
            created.transform.SetParent(this.transform);
        }
    }
}
