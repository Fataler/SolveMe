using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Level27 : MonoBehaviour
{
    [System.Serializable]
    public class Puzzle
    {
        public int winConnections;
        public int curConnections;

        public int width;
        public int height;

        public RotatableTile[,] tiles;
    }

    public Puzzle puzzle;
    public GameObject solve;

    private void Start()

    {
        Vector2 dimensions = CheckDimensions();

        puzzle.width = (int)dimensions.x;
        puzzle.height = (int)dimensions.y;
        puzzle.tiles = new RotatableTile[puzzle.width, puzzle.height];
        foreach (var tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            var tileScript = tile.GetComponent<RotatableTile>();
            puzzle.tiles[(int)tile.transform.position.x, (int)tile.transform.position.y] = tileScript;
            puzzle.winConnections += (int)tileScript.tileType / 2;
        }

        Shuffle();
        puzzle.curConnections = Sweep();
    }

    private Vector2 CheckDimensions()
    {
        Vector2 aux = Vector2.zero;

        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Tile");

        foreach (var p in pieces)
        {
            if (p.transform.position.x > aux.x)
                aux.x = p.transform.position.x;

            if (p.transform.position.y > aux.y)
                aux.y = p.transform.position.y;
        }

        aux.x++;
        aux.y++;

        return aux;
    }

    private void Shuffle()
    {
        foreach (var piece in puzzle.tiles)
        {
            int k = UnityEngine.Random.Range(0, 4);

            for (int i = 0; i < k; i++)
            {
                piece.RotatePiece();
            }
        }
    }

    public int Sweep()
    {
        int value = 0;

        for (int h = 0; h < puzzle.height; h++)
        {
            for (int w = 0; w < puzzle.width; w++)
            {
                //compares top
                if (h != puzzle.height - 1)
                    if (puzzle.tiles[w, h].values[0] == 1 && puzzle.tiles[w, h + 1].values[2] == 1)
                        value++;

                //compare right
                if (w != puzzle.width - 1)
                    if (puzzle.tiles[w, h].values[1] == 1 && puzzle.tiles[w + 1, h].values[3] == 1)
                        value++;
            }
        }

        return value;
    }

    public int QuickSweep(int w, int h)
    {
        int value = 0;

        //compares top
        if (h != puzzle.height - 1)
            if (puzzle.tiles[w, h].values[0] == 1 && puzzle.tiles[w, h + 1].values[2] == 1)
                value++;

        //compare right
        if (w != puzzle.width - 1)
            if (puzzle.tiles[w, h].values[1] == 1 && puzzle.tiles[w + 1, h].values[3] == 1)
                value++;

        //compare left
        if (w != 0)
            if (puzzle.tiles[w, h].values[3] == 1 && puzzle.tiles[w - 1, h].values[1] == 1)
                value++;

        //compare bottom
        if (h != 0)
            if (puzzle.tiles[w, h].values[2] == 1 && puzzle.tiles[w, h - 1].values[0] == 1)
                value++;

        return value;
    }

    internal void Win()
    {
        Debug.Log("Win");
        GameManager.instance.NextLevel();
        /*foreach (var item in puzzle.tiles)
        {
            if (item.name == "Tile_DoubleQuater_90" && item.transform.rotation.eulerAngles.z != 90)
            {
                Debug.Log(item.name + "break" + item.transform.position.ToString() + item.transform.rotation.eulerAngles.z.ToString());
                check = false;
                break;
            }
            else if (item.name == "Tile_DoubleQuater_180")
            {
                if (Math.Abs(item.transform.rotation.eulerAngles.z) == 180 || Math.Abs(item.transform.rotation.eulerAngles.z) == 270)
                {
                    Debug.Log(item.name + "gg" + item.transform.position.ToString() + item.transform.rotation.eulerAngles);
                    check = false;
                    break;
                }
            }
        }
        if (check)
        {
            Debug.Log("Win");
        }*/
    }
}