using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingFactory : MonoBehaviour
{
    public GameObject[] Toppings;
    private Canvas canvas;
    private SpriteRenderer sprite;
    private System.Random _rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
        sprite = GetComponent<SpriteRenderer>();
        Fill();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(Time.deltaTime, 0, 0);   
    }

    public void Fill(){
        if(Toppings.Length > 0){
            var index = _rand.Next(Toppings.Length);
            var created = Instantiate(Toppings[index], this.gameObject.transform.position, Quaternion.identity);
            created.transform.SetParent(canvas.transform);
            var topping = created.GetComponent<Topping>();
            this.transform.position -= new Vector3(topping.Width/2.0f + 0.8f, 0, 0);
            sprite.size = new Vector2(topping.Width + 0.5f, topping.Height + 0.5f);
        } else {
            Debug.Log("Factory has no things in registered in for making");
        }
    }
}
