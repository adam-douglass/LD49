using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingFactory : MonoBehaviour
{
    public GameObject[] Toppings;
    private Canvas canvas;
    private SpriteRenderer sprite;

    private Randwhich rand;
    public int Width;
    private const int WorldWidth = 15;
    private float velocity = 0;

    public static HashSet<FlavourKinds> ActiveFlavours = new HashSet<FlavourKinds>();
    public static void AddMoreFlavour(){
        var options = new List<FlavourKinds>();
        foreach(var value in System.Enum.GetValues(typeof(FlavourKinds))){
            var kind = (FlavourKinds)value;
            if(!ActiveFlavours.Contains(kind)){
                options.Add(kind);
            }
        }
        if(options.Count == 0) return;
        var rand = new System.Random();
        ActiveFlavours.Add(options[rand.Next(options.Count)]);
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
        sprite = GetComponent<SpriteRenderer>();
        rand = FindObjectOfType<ToppingCarosel>().Rand;
        Fill();
    }

    // Update is called once per frame
    void Update()
    {
        if(SessionInfo.Singleton.gameMode == GameMode.Puzzle){
            if(this.Right >= WorldWidth){
                if(!this.gameObject.GetComponentInChildren<Topping>())
                    this.transform.position += new Vector3(Time.deltaTime*3.0f, 0, 0);
            } else {
                velocity += Time.deltaTime/5.0f;
                if(CanMoveRight(velocity)){
                    this.transform.position += new Vector3(velocity, 0, 0);
                } else {
                    velocity = 0;
                }
            }
        } else {
            this.transform.position += new Vector3(Time.deltaTime, 0, 0);   
        }
    }

    public bool CanMoveRight(float delta) {
        var right = this.Right;
        foreach(var other in GameObject.FindObjectsOfType<ToppingFactory>()){
            if(other == this) continue;
            if(other.transform.position.x < this.transform.position.x) continue;
            if(other.Left < Right + delta) return false;
        }
        return true;
    }

    public float Right {
        get => this.transform.position.x + Width/2.0f + 0.5f;
    }

    public float Left {
        get => this.transform.position.x - Width/2.0f - 0.5f;
    }

    public void Fill(){
        if(Toppings.Length > 0){
            var index = rand.GetRandom(Toppings.Length);
            while (true){
                var good = false;
                foreach(var flav in Toppings[index].GetComponent<Topping>().Flavours){
                    if(ActiveFlavours.Contains(flav)){
                        good = true;
                        break;
                    }
                }

                if(good) break;
                index = rand.GetRandom(Toppings.Length);
            }
            
            var created = Instantiate(Toppings[index], this.gameObject.transform.position, Quaternion.identity);
            created.transform.SetParent(canvas.transform);
            var topping = created.GetComponent<Topping>();
            this.transform.position -= new Vector3(topping.Width/2.0f + 0.8f, 0, 0);
            this.Width = topping.Width;
            sprite.size = new Vector2(topping.Width + 0.5f, topping.Height + 0.5f);
        } else {
            Debug.Log("Factory has no things in registered in for making");
        }
    }
}
