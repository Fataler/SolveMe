using System.Collections.Generic;
using UnityEngine;

public class Level16 : MonoBehaviour
{
    public List<GameObject> lines;
    private int counter = 0;
    public List<GameObject> solution;

    private void Start()
    {
        ClickListener.ObjClicked += CheckMove;
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckMove;
    }

    private void CheckMove(GameObject go)
    {
        if (ReferenceEquals(go, solution[counter]))
        {
            lines[counter].SetActive(true);
            counter++;
        }
        else
        {
            counter = 0;
            lines.ForEach(x => x.SetActive(false));
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if (counter == solution.Count)
        {
            Debug.Log("Win");
            ClickListener.ObjClicked -= CheckMove;
            GameManager.instance.NextLevel();
        }
    }
}