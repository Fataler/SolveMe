using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Level40 : MonoBehaviour
{
    public List<ClickBehaviour> solution;
    private int counter = 0;

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
        var itemScript = go.GetComponent<ClickBehaviour>();
        if (itemScript.name == solution[counter].name)
        {
            itemScript.ToggleHold();
            counter++;
        }
        else
        {
            counter = 0;
            Utilites.ResetButtonsState(solution);
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

    // Update is called once per frame
    private void Update()
    {
    }
}