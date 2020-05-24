using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The goal is to make all items holded.
/// All positions of clicks are inverted
/// </summary>
public class Level7 : MonoBehaviour
{
    public List<ClickBehaviour> buttons;

    private void Start()
    {
        ClickListener.ObjClicked += CheckMove;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Button");

        foreach (var obj in objects)
        {
            ClickBehaviour clickBehaviour = obj.GetComponent<ClickBehaviour>();
            buttons.Add(clickBehaviour);
            //Debug.Log($"{GetInvertedPosition(clickBehaviour.transform.position)} - {obj.name}");
        }
    }

    private void CheckMove(GameObject go)
    {
        foreach (var item in buttons)
        {
            if (GetInvertedPosition(go.transform.localPosition) == item.transform.localPosition)
            {
                item.hold = !item.hold;
            }
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if (Utilites.AllItemsHolded(buttons))
        {
            Debug.Log("Win!");
            GameManager.instance.NextLevel();
        }
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckMove;
    }

    private Vector3 GetInvertedPosition(Vector3 position)
    {
        return position * -1;
    }
}