using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level32 : MonoBehaviour
{
    public List<ClickBehaviour> dices;
    private ClickBehaviour curDice = null;
    private ClickBehaviour lastDice;
    private int dicesLength;
    private int lastDiceIndex;

    private void Start()
    {
        ClickListener.ObjClicked += CheckClick;
        dicesLength = dices.Count;
    }

    private void CheckClick(GameObject go)
    {
        var item = go.GetComponent<ClickBehaviour>();
        curDice = item;

        if (lastDice == null)
        {
            lastDice = item;
            var diceI = dices.Find(diceItem => ReferenceEquals(diceItem, lastDice));
            diceI.ToggleHold();
            lastDiceIndex = dices.IndexOf(diceI);
            return;
        }
        int lastDiceValue = int.Parse(lastDice.name);
        lastDiceIndex = dices.IndexOf(lastDice);
        var moveTo = lastDiceIndex + lastDiceValue;
        if (moveTo >= dicesLength)
            moveTo = Mathf.Abs(dicesLength - moveTo);

        if (ReferenceEquals(curDice, dices[moveTo]))
        {
            curDice.ToggleHold();
            lastDice = curDice;
            lastDiceIndex = dices.IndexOf(curDice);
        }
        else
        {
            lastDice = null;
            Utilites.ResetButtonsState(dices);
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if (Utilites.AllItemsHolded(dices))
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