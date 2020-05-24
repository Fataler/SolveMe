using System;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    public List<GameObject> solution;
    public List<GameObject> objs;
    public int curBlock = 0;

    private void Start()
    {
        ClickListener.ObjClicked += CheckMove;
    }

    public void CheckMove(GameObject go)
    {
        var goScript = go.GetComponent<ClickBehaviour>();
        if (solution[curBlock] == go)
        {
            objs.Add(go);
            curBlock++;
            goScript.hold = true;
        }
        else
        {
            curBlock = 0;
            objs = new List<GameObject>();
            goScript.ResetSprite();
            foreach (GameObject item in solution)
            {
                var config = item.GetComponent<ClickBehaviour>();
                config.ResetSprite();
            }
            AudioManager.instance.Play("Reset");
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if (solution.Count == objs.Count)
        {
            Debug.Log("win");
            GameManager.instance.NextLevel();
        }
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckMove;
    }
}