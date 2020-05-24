using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool isDragging;
    private float startPosx;
    private float startPosy;
    public float maxX;
    public float maxY;

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            transform.localPosition = new Vector3(Mathf.Round((mousePos.x - startPosx) * 10) / 10,
                (Mathf.Round((mousePos.y - startPosy) * 10) / 10));
            Vector3 positionFixer = new Vector3(Mathf.Clamp(transform.localPosition.x, -maxX, maxX),
                Mathf.Clamp(transform.localPosition.y, -maxY, maxY));

            transform.position = positionFixer;
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        startPosx = mousePos.x - this.transform.localPosition.x;
        startPosy = mousePos.y - this.transform.localPosition.y;
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}