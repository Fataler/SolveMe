using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableTile : MonoBehaviour
{
    public enum Type
    {
        Quater = 2,
        Line = 2,
        DoubleQuater = 4,
        Epmty = 0
    }

    public Type tileType;

    public Sprite QuaterSprite;
    public Sprite DoubleQuaterSprite;
    public Sprite LineSprite;
    public Sprite EmptySprite;

    private float realRotation;
    private float speed = 0.5f;

    public int[] values;

    private Level27 solution;

    private void Start()
    {
        solution = (Level27)FindObjectOfType(typeof(Level27));
    }

    /*private void Init()
    {
        Sprite sprite = null;
        switch (tileType)
        {
            case Type.Quater:
                sprite = QuaterSprite;
                break;

            case Type.DoubleQuater:
                sprite = DoubleQuaterSprite;
                break;

            case Type.Epmty:
                sprite = EmptySprite;
                break;

            case Type.Line:
                sprite = LineSprite;
                break;
        }
        GetComponent<SpriteRenderer>().sprite = sprite;
    }*/

    private void OnMouseDown()
    {
        int difference = -solution.QuickSweep((int)transform.position.x, (int)transform.position.y);

        RotatePiece();

        difference += solution.QuickSweep((int)transform.position.x, (int)transform.position.y);

        solution.puzzle.curConnections += difference;

        if (solution.puzzle.curConnections == solution.puzzle.winConnections)
            solution.Win();
    }

    public void RotatePiece()
    {
        realRotation += 90;

        if (realRotation == 360)
            realRotation = 0;
        RotateValues();
    }

    public void RotateValues()
    {
        int aux = values[0];

        for (int i = 0; i < values.Length - 1; i++)
        {
            values[i] = values[i + 1];
        }
        values[3] = aux;
    }

    private void Update()
    {
        if (transform.eulerAngles.z != realRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, realRotation), speed);
        }
    }
}