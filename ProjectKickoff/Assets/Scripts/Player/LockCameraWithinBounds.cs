using UnityEngine;

public class LockCameraWithinBounds : MonoBehaviour
{
    RectInt levelBounds;
    Camera cam;
    void Start()
    {
        levelBounds = LevelData.instance.levelBounds;
        cam = this.GetComponent<Camera>();
    }

    void Update()
    {
        float zoomW = cam.orthographicSize;
        float zoomH = cam.orthographicSize / ((float)Screen.height / Screen.width);
        transform.position = new(Mathf.Clamp(transform.parent.position.x, levelBounds.xMin + zoomH, levelBounds.xMax - zoomH)
            , Mathf.Clamp(transform.parent.position.y, levelBounds.yMin + zoomW, levelBounds.yMax - zoomW), transform.position.z);
    }
}
