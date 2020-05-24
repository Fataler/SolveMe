using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MovableObject : MonoBehaviour
{
    public bool Move(Vector2 dir)
    {
        //dir.Normalize();
        if (Blocked(transform.position, dir))
        {
            return false;
        }
        else
        {
            transform.Translate(dir);
            return true;
        }
    }

    private bool Blocked(Vector3 pos, Vector2 dir)
    {
        Vector2 newPos = new Vector2(pos.x, pos.y) + dir;

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");

        foreach (var obj in objects)
        {
            MovableObject movableObject = obj.GetComponent<MovableObject>();

            if (obj.transform.position.x == newPos.x && obj.transform.position.y == newPos.y && name != obj.name && movableObject != null)
            {
                movableObject.Move(dir);
                return false;
            }
        }
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var item in walls)
        {
            if (item.transform.position.x == newPos.x && item.transform.position.y == newPos.y)
            {
                return true;
            }
        }

        return false;
    }
}