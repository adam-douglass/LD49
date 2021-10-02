using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Topping : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    public int Width;
    public int Height;

    public static Topping Dragging;
    private Vector3 mouseLocation;

    // Start is called before the first frame update
    void Start()
    {   
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Dragging == this)
            SnapOnto();
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("Begin Drag");
        startPosition = this.transform.position;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        Dragging = this;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("End Drag");
        this.transform.position = startPosition;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        Dragging = null;
    }

    private void SnapOnto(){
        var pos = new Vector3(mouseLocation.x, mouseLocation.y, mouseLocation.z);

        var hits = Physics2D.RaycastAll(pos, Vector2.up);

        GameObject grid = null;

        foreach(var hit in hits){
            if(hit.collider == null) continue;
            if(hit.transform.gameObject == this.gameObject) continue;
            if(hit.transform.gameObject.GetComponent<GridControl>() != null){
                grid = hit.transform.gameObject;
                break;
            }
        }

        var offset = new Vector3(0, 0, 0);
        if((Width & 1) == 0){
            offset.x = 0.5f;
        }
        if((Height & 1) == 0){
            offset.y = 0.5f;
        }

        if(grid == null){
            this.transform.position = pos;
        } else {
            var local = pos - grid.transform.position;
            local.x = Mathf.Round(local.x);
            local.y = Mathf.Round(local.y);

            Debug.Log(local);
            this.transform.position = local + grid.transform.position; // + offset;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        mouseLocation = eventData.pointerCurrentRaycast.worldPosition;
        SnapOnto();
    }

}
