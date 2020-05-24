using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level28 : MonoBehaviour
{
    public List<GameObject> dots;
    public List<GameObject> solution;
    public GameObject buttonL;
    public GameObject buttonR;
    private int counter = 0;

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
        if (ReferenceEquals(go, solution[counter]))
        {
            dots[counter].SetActive(false);
            counter++;
        }
        else
        {
            counter = 0;
            dots.ForEach(x => x.SetActive(true));
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