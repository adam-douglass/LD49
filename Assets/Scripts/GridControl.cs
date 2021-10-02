using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridControl : MonoBehaviour, IDropHandler
{
    public int Width;
    public int Height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, -1, 0) * Time.deltaTime*0.05f;
    }

    public void OnDrop(PointerEventData eventData){
        
    }
}
