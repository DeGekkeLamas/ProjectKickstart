using UnityEngine;

public class CanvasCamSetter : MonoBehaviour
{
    Canvas canvas;
    private void Awake()
    {
        canvas = this.GetComponent<Canvas>();
    }

    private void Update()
    {
        canvas.worldCamera = Camera.main;
    }
}
