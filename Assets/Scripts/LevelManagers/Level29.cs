using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level29 : MonoBehaviour
{
    public GameObject trueButton;
    private int counter = 0;
    private GameObject[] buttons;

    private void Start()
    {
        ClickListener.ObjClicked += CheckClick;
        buttons = GameObject.FindGameObjectsWithTag("Button");
    }

    private void CheckClick(GameObject go)
    {
        if (ReferenceEquals(go, trueButton))
        {
            buttons[counter].GetComponent<ClickBehaviour>().hold = true;
            counter++;
            if (counter > buttons.Length)
                counter = buttons.Length;
        }
        else
        {
            counter = 0;
            Utilites.ResetButtonsState(buttons);
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if (counter == buttons.Length)
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckClick;
    }
}