using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridControl : MonoBehaviour
{
    public int Width;
    public int Height;
    public Collider2D Collider;
    public FlavourKinds[] Flavours;

    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<Collider2D>();
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        int target = Random.Range(1, 3);
        var selected = new HashSet<FlavourKinds>();
        var values = System.Enum.GetValues(typeof(FlavourKinds));
        Flavours = new FlavourKinds[target];
        
        while(selected.Count < target){
            var pick = (FlavourKinds)values.GetValue(Random.Range(0, values.Length));
            if(!selected.Contains(pick)){
                Flavours[selected.Count] = pick;
                selected.Add(pick);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 Offset {
        get => new Vector2(Width, Height)/2.0f;        
    }

    public Vector3 Origin {
        get => (Collider.bounds.center - new Vector3(Width, Height, 0)/2.0f);
    }

    public Vector2 Origin2 {
        get => (new Vector2(Collider.bounds.center.x, Collider.bounds.center.y) - new Vector2(Width, Height)/2.0f);
    }

    public Vector2Int SnapTo(Vector2 point, int width, int height){
        int left = System.Math.Min(System.Math.Max(0, Mathf.RoundToInt(point.x - (float)width/2.0f)), Width - width);
        int down = System.Math.Min(System.Math.Max(0, Mathf.RoundToInt(point.y - (float)height/2.0f)), Height - height);
        //Debug.Log($"{0}, {Mathf.RoundToInt(point.y - (float)height/2.0f)}, {Height - height}, {down}");
        return new Vector2Int(left, down);
    }

    public bool AddObject(Topping topping, Vector2Int offset){
        if(AreaClearFor(offset, topping.Width, topping.Height, topping)){
            topping.transform.SetParent(this.transform, false);
            topping.transform.position = offset + Origin2 + topping.Offset;
            // topping.transform.localPosition = offset + topping.Offset;
            return true;
        }
        return false;
    }

    public bool AreaClearFor(Vector2Int index, int width, int height, Topping looking){
        if(looking.Mask.Length > 0){
            foreach(var offset in looking.Mask){
                var obj = ObjectAt(index + offset);
                if(obj != null && obj != looking) return false;
            }
        } else {
            for(int ii = 0; ii < width; ii++){
                for(int jj = 0; jj < height; jj++){
                    var obj = ObjectAt(index + new Vector2Int(ii, jj));
                    if(obj != null && obj != looking) return false;
                }
            }
        }
        return true;
    }

    public bool AreaClear(Vector2Int index, int width, int height){
        for(int ii = 0; ii < width; ii++){
            for(int jj = 0; jj < height; jj++){
                if(ObjectAt(index + new Vector2Int(ii, jj))) return false;
            }
        }
        return true;
    }

    public Topping ObjectAt(Vector2Int point) {
        foreach(var option in GetComponentsInChildren<Topping>()){
            var index = Vector2Int.RoundToInt(option.transform.position - Origin - option.Offset3);
            if(option.Mask.Length > 0){
                foreach(var offset in option.Mask){
                    if(index + offset == point){
                        return option;
                    }
                }
            } else {
                if(index.x <= point.x && point.x < index.x + option.Width){
                    if(index.y <= point.y && point.y < index.y + option.Height){
                        return option;
                    }
                }
            }
        }
        return null;
    }

    public bool HasFlavour(FlavourKinds flavour){
        foreach(var option in GetComponentsInChildren<Topping>()){
            if(option.HasFlavour(flavour)){
                return true;
            }
        }
        return false;
    }
}
