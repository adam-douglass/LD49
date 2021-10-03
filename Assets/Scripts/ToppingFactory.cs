using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingFactory : MonoBehaviour
{
    public GameObject[] Toppings;
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
        Fill();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(Time.deltaTime * 0.3f, 0, 0);   
    }

    public void Fill(){
        if(Toppings.Length > 0){
            var index = Random.Range(0, Toppings.Length);
            var created = Instantiate(Toppings[index], this.gameObject.transform.position, Quaternion.identity);
            created.transform.SetParent(this.canvas.transform);
        } else {
            Debug.Log("Factory has no things in registered in for making");
        }
    }
}
