using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera camera;
    private int TargetWidth = 30;
    private int TargetHeight = 20;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        var bottom = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        var top = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        var width = top.x - bottom.x;
        var height = top.y - bottom.y;

        var ideal_width_zoom = TargetWidth/width * camera.orthographicSize;
        var ideal_height_zoom = TargetHeight/height * camera.orthographicSize;
        camera.orthographicSize = Mathf.Max(ideal_width_zoom, ideal_height_zoom);
    }
}
