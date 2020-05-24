using System;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public List<GameObject> bgs;
    private int currentBg = -1;

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
        currentBg++;
        CheckWin();
    }

    private void CheckWin()
    {
        if (currentBg >= 0)
        {
            bgs[currentBg].SetActive(true);
        }
        if (bgs[bgs.Count - 1].activeSelf == true)
        {
            //StartCoroutine(Utils.TakeScreenshot());
            Debug.Log("Completed");
            GameManager.instance.NextLevel();
        }
    }
}