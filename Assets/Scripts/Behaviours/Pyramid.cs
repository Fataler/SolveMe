using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pyramid : MonoBehaviour
{
    public enum blockColor
    {
        Red,
        Green,
        Blue
    }

    public Stack<GameObject> items;
    public blockColor color;

    public bool CheckWin()
    {
        var solution = new List<string>() { color.ToString() + "_Top", color.ToString() + "_Mid", color.ToString() + "_Bot" };
        var itemsList = new List<string>();
        foreach (var item in items)
        {
            itemsList.Add(item.name);
        }

        if (solution.SequenceEqual(itemsList))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}