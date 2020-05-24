using UnityEngine;

public class MouseToClickConverter : MonoBehaviour
{
    public GameObject hoveredGO;

    public enum HoverState { HOVER, NONE };

    public HoverState hover_state = HoverState.NONE;

    private void Update()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hover_state == HoverState.NONE)
            {
                hitInfo.collider.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);
                hoveredGO = hitInfo.collider.gameObject;
            }
            hover_state = HoverState.HOVER;
        }
        else
        {
            if (hover_state == HoverState.HOVER)
            {
                hoveredGO.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
            }
            hover_state = HoverState.NONE;
        }

        if (hover_state == HoverState.HOVER)
        {
            hitInfo.collider.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver); //Mouse is hovering
            if (Input.GetMouseButtonDown(0))
            {
                hitInfo.collider.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver); //Mouse down
            }
            if (Input.GetMouseButtonUp(0))
            {
                hitInfo.collider.SendMessage("OnMouseUp", SendMessageOptions.DontRequireReceiver); //Mouse up
            }
        }
    }
}