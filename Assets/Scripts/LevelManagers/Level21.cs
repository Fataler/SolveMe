using UnityEngine;

public class Level21 : MonoBehaviour
{
    public GameObject[] buttons;
    private int counter = 0;
    private GameObject lastGo;

    // Start is called before the first frame update
    private void Start()
    {
        ClickListener.ObjClicked += CheckClick;
        buttons = GameObject.FindGameObjectsWithTag("Button");
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckClick;
    }

    private void CheckClick(GameObject go)
    {
        ClickBehaviour clickBehaviour = go.GetComponent<ClickBehaviour>();
        if (clickBehaviour.hold)
        {
            ResetTable();
            return;
        }
        clickBehaviour.hold = true;
        if (!lastGo)
        {
            counter++;
            lastGo = go;
            return;
        }

        var goPos = go.transform.localPosition;
        var lastPos = lastGo.transform.localPosition;
        switch (lastGo.name)
        {
            case "D":
                if (lastPos.x == goPos.x && lastPos.y > goPos.y)
                {
                    DoAction(go);
                }
                else
                {
                    ResetTable();
                }
                break;

            case "U":
                if (lastPos.x == goPos.x && lastPos.y < goPos.y)
                {
                    DoAction(go);
                }
                else
                {
                    ResetTable();
                }
                break;

            case "R":
                if (lastPos.x < goPos.x && lastPos.y == goPos.y)
                {
                    DoAction(go);
                }
                else
                {
                    ResetTable();
                }
                break;

            case "L":
                if (lastPos.x > goPos.x && lastPos.y == goPos.y)
                {
                    DoAction(go);
                }
                else
                {
                    ResetTable();
                }
                break;

            case "Cross":
                ResetTable();
                break;
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if (counter == buttons.Length)
        {
            Debug.Log("Win!");
            GameManager.instance.NextLevel();
        }
    }

    private void ResetTable()
    {
        lastGo = null;
        counter = 0;
        Utilites.ResetButtonsState(buttons);
    }

    public void DoAction(GameObject go)
    {
        lastGo = go;
        counter++;
    }
}