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

    // Start is called before the first frame update
    void Start()
    {   
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("Begin Drag");
        startPosition = this.transform.position;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("End Drag");
        this.transform.position = startPosition;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }

    public void OnDrag(PointerEventData eventData) {
        var pos = eventData.pointerCurrentRaycast.worldPosition;
        Debug.Log($"World {pos}");
        var offset = new Vector3(0, 0, 0);
        if((Width & 1) == 0){
            offset.x = 0.5f;
        }
        if((Height & 1) == 0){
            offset.y = 0.5f;
        }
        this.transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z)) + offset;
    }

}
