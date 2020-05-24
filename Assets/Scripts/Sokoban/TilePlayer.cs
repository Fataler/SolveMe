using UnityEngine;

public class TilePlayer : MonoBehaviour
{
    public bool blockedByTile = false;

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

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("TilePlayer");
        foreach (var player in players)
        {
            //TilePlayer tilePlayer = player.GetComponent<TilePlayer>();

            if (player.transform.position.x == newPos.x && player.transform.position.y == newPos.y)
            {
                blockedByTile = true;
                return true;
            }
        }

        return false;
    }
}