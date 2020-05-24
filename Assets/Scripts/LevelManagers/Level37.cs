using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level37 : MonoBehaviour
{
    public List<GameObject> solution;
    public int counter = 0;

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckClick;
    }

    private void Start()
    {
        ClickListener.ObjClicked += CheckClick;
    }

    private void CheckClick(GameObject go)
    {
        if (ReferenceEquals(go, solution[counter]))
        {
            counter++;
        }
        else
        {
            counter = 0;
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if (counter == solution.Count)
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }
}