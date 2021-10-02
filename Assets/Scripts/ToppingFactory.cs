using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingFactory : MonoBehaviour
{
    public GameObject[] Toppings;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        Fill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fill(){
        var created = Instantiate(Toppings[1], this.gameObject.transform.position, Quaternion.identity);
        created.transform.SetParent(canvas.transform);
    }
}
