using UnityEngine;

public class LockCameraWithinBounds : MonoBehaviour
{
    RectInt levelBounds;
    Camera cam;
    float maxZoom;

    void Start()
    {
        levelBounds = LevelData.instance.levelBounds;
        cam = GetComponent<Camera>();

        // Calculate maxZoom based on level bounds and screen aspect ratio
        float aspectRatio = (float)Screen.width / Screen.height;
        float maxZoomVertical = levelBounds.height / 2f;
        float maxZoomHorizontal = levelBounds.width / (2f * aspectRatio);
        maxZoom = Mathf.Min(maxZoomVertical, maxZoomHorizontal);
    }

    void Update()
    {
        // zoom
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scrollInput) > 0.01f)
        {
            cam.orthographicSize -= scrollInput;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 1, maxZoom);
        }

        //fix bounds
        float zoomW = cam.orthographicSize;
        float zoomH = cam.orthographicSize / ((float)Screen.height / Screen.width);
        transform.position = new(Mathf.Clamp(transform.parent.position.x, levelBounds.xMin + zoomH, levelBounds.xMax - zoomH)
            , Mathf.Clamp(transform.parent.position.y, levelBounds.yMin + zoomW, levelBounds.yMax - zoomW), transform.position.z);
    }
}
