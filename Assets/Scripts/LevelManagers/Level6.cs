using System.Collections.Generic;
using UnityEngine;

public class Level6 : MonoBehaviour
{
    public List<ClickBehaviour> buttons = new List<ClickBehaviour>();

    private GameObject lastGo;
    public int counter = 0;
    private int totalButtons;

    private void Start()
    {
        ClickListener.ObjClicked += CheckMove;
        var GameObjects = GameObject.FindGameObjectsWithTag("Button");
        foreach (var go in GameObjects)
        {
            buttons.Add(go.GetComponent<ClickBehaviour>());
        }
        totalButtons = buttons.Count;
    }

    private void CheckMove(GameObject go)
    {
        ClickBehaviour clickBehaviour = go.GetComponent<ClickBehaviour>();
        if (!lastGo)
        {
            lastGo = go;
            counter++;
            clickBehaviour.hold = true;

            return;
        }
        if (Utilites.GetLocalNeighbourPositions(go).Contains(lastGo.transform.localPosition))
        {
            lastGo = go;

            clickBehaviour.hold = !clickBehaviour.hold;

            counter++;
        }
        else
        {
            lastGo = null;
            counter = 0;
            Utilites.ResetButtonsState(buttons);
        }
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckMove;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Utilites.AllItemsHolded(buttons))
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }
}