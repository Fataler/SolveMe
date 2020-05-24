using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level34 : MonoBehaviour
{
    public List<ClickBehaviour> clicks;

    private void Start()
    {
        ClickListener.ObjClicked += CheckClick;
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckClick;
    }

    private void CheckClick(GameObject go)
    {
        var click = go.GetComponent<ClickBehaviour>();
        click.clickCounter++;
        var valueToCheck = int.Parse(click.name);
        if (click.clickCounter > valueToCheck)
        {
            Utilites.ResetButtonsState(clicks);
            clicks.ForEach(item => item.ResetClicks());
            return;
        }
        if (click.clickCounter == valueToCheck)
        {
            click.ToggleHold();
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if (Utilites.AllItemsHolded(clicks))
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }
}