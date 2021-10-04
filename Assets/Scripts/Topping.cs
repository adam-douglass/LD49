using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum FlavourKinds {
    Healthy,
    Boring,
    Salty,
    Vegetarian,
    Fibrous,
    Dangerous,
    Sharp,
    Clean,
    Crunchy,
    Sticky,
    Savoury,
    Hot,
    Fuzzy,
    Exotic,
    Extravagant,
    Slimey,
    Fragile,
}

public class Topping : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    public int Width;
    public int Height;
    public FlavourKinds[] Flavours;
    public Vector2Int[] Mask;

    private bool dragging;
    private Vector3 originPosition;
    private Vector3 mouseLocation;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dragging) SnapOnto();
    }

    public bool HasFlavour(FlavourKinds flavour){
        foreach(var option in Flavours){
            if(option == flavour) return true;
        }
        return false;
    }

    public Vector2 Offset {
        get => new Vector2(Width, Height)/2.0f;        
    }
    public Vector3 Offset3 {
        get => new Vector3(Width, Height, 0)/2.0f;        
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("Begin Drag");
        startPosition = this.transform.position;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        dragging = true;
        originPosition = this.transform.parent.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("End Drag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        dragging = false;

        var grid = SnapTarget();
        if(grid){
            if(grid.AddObject(this, SnapLocation(grid))){
                return;
            }
        }
        this.transform.position = startPosition + this.transform.parent.position - originPosition;
    }

    private GridControl SnapTarget(){
        var pos = new Vector3(mouseLocation.x, mouseLocation.y, mouseLocation.z);

        GridControl grid = null;
        foreach(var maybe in GameObject.FindObjectsOfType<GridControl>()){
            if(maybe.Collider.bounds.Contains(pos) && maybe.Fits(Width, Height)){
                grid = maybe;
            }
        }

        return grid;
    }

    private Vector2Int SnapLocation(GridControl grid){
        var here = mouseLocation - grid.Origin;
        var local = grid.SnapTo(here, Width, Height);
        return local;
    }

    private void SnapOnto(){
        var grid = SnapTarget();
        var pos = new Vector3(mouseLocation.x, mouseLocation.y, mouseLocation.z);
        var offset = new Vector3(((float)Width)/2.0f, ((float)Height)/2.0f, 0);

        if(grid == null){
            this.transform.position = pos;
        } else {
            var grid_offset = (grid.Collider.bounds.center - new Vector3(grid.Width, grid.Height, 0)/2.0f);
            var local = SnapLocation(grid);
            this.transform.position = new Vector3(local.x, local.y, 0) + grid_offset + offset;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseLocation.z = 0;
        SnapOnto();
    }

}
