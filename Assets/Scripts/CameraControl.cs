using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera _camera;
    private int TargetWidth = 30;
    private int TargetHeight = 20;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        var bottom = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        var top = _camera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        var width = top.x - bottom.x;
        var height = top.y - bottom.y;

        var ideal_width_zoom = TargetWidth/width * _camera.orthographicSize;
        var ideal_height_zoom = TargetHeight/height * _camera.orthographicSize;
        _camera.orthographicSize = Mathf.Max(ideal_width_zoom, ideal_height_zoom);
    }
}
