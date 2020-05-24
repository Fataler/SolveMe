using UnityEngine;

public class Scaler : MonoBehaviour
{
    public float aspect_w = 9f;
    public float aspect_h = 16f;

    private void Start()
    {
        Camera cam = GetComponent<Camera>();

        var targetAspect = aspect_w / aspect_h;
        var initialSize = cam.orthographicSize;
        if (cam.aspect < 1.6f)
        {
            // respect width (modify default behavior)
            cam.orthographicSize = initialSize * (targetAspect / Camera.main.aspect);
        }
        else
        {
            // respect height (change back to default behavior)
            cam.orthographicSize = initialSize;
        }
    }
}