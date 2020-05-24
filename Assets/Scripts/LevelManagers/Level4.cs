using System;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    public List<GameObject> solution;

    [SerializeField]
    private int counter = 0;

    [SerializeField]
    private int reverseCounter;

    // Start is called before the first frame update
    public void Start()
    {
        ClickListener.ObjClicked += CheckMove;
        reverseCounter = solution.Count - 1;
    }

    public void CheckMove(GameObject go)
    {
        if (ReferenceEquals(solution[counter].gameObject, go) || ReferenceEquals(solution[reverseCounter].gameObject, go))
        {
            counter++;
            reverseCounter--;
            go.GetComponent<ClickBehaviour>().ToggleHold();
        }
        else
        {
            counter = 0;
            reverseCounter = solution.Count - 1;
            Utilites.ResetButtonsState(solution.ToArray());
            AudioManager.instance.Play("Reset");
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if ((counter == solution.Count) || (reverseCounter == -1))
        {
            Debug.Log("win");
            GameManager.instance.NextLevel();
        }
    }

    public void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckMove;
    }
}