using System.Collections.Generic;
using UnityEngine;

public class Level17 : MonoBehaviour
{
    public ClickBehaviour[] buttons;

    private void Start()
    {
        buttons = FindObjectsOfType<ClickBehaviour>();
        ClickListener.ObjClicked += CheckMove;
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckMove;
    }

    private void CheckMove(GameObject go)
    {
        var script = go.GetComponent<ClickBehaviour>();
        script.hold = !script.hold;
        var localNeighbours = Utilites.GetLocalNeighbourPositions(go);

        foreach (var position in localNeighbours)
        {
            foreach (var item in buttons)
            {
                if (item.transform.localPosition == position)
                {
                    var itemScript = item.GetComponent<ClickBehaviour>();
                    itemScript.hold = !itemScript.hold;
                }
            }
        }
        CheckWin();
    }

    private void CheckWin()
    {
        var list = new List<ClickBehaviour>(buttons);
        if (Utilites.AllItemsHolded(list))
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }
}