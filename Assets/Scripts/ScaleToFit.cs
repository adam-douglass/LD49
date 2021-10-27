using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToFit : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var height = Camera.main.orthographicSize * 2.0;
        var width = height * Screen.width / Screen.height;
        //transform.localScale
        
        var bounds = GetComponentInChildren<SpriteRenderer>().bounds;
        var grow_height = height / bounds.size.y;
        var grow_width = width / bounds.size.x;
        var scale = (float)System.Math.Min(grow_height, grow_width) * transform.localScale.x;
        transform.localScale = new Vector3(scale, scale, 1);
    }
}