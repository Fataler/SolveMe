using System.Collections.Generic;
using UnityEngine;

public class Level14 : MonoBehaviour
{
    public List<Sprite> firstPart;
    public List<Sprite> secondPart;

    public List<int> counters;
    public GameObject button;
    public SpriteRenderer noFirstPart;
    public SpriteRenderer noSecondPart;
    public SpriteRenderer ntFirstPart;
    public SpriteRenderer ntSecondPart;

    private void Start()
    {
        ClickListener.ObjClicked += CheckMove;
        ClickListener.ObjClicked += CheckSpites;
    }

    private void CheckSpites(GameObject obj)
    {
        noFirstPart.sprite = firstPart[counters[0]];
        noSecondPart.sprite = secondPart[counters[1]];
        ntFirstPart.sprite = firstPart[counters[2]];
        ntSecondPart.sprite = secondPart[counters[3]];
    }

    private void CheckMove(GameObject go)
    {
        if (go.name == noFirstPart.name)
        {
            counters[2]++;
            counters[1]++;
        }
        else if (go.name == noSecondPart.name)
        {
            counters[0]++;
            counters[3]++;
        }
        else if (go.name == ntFirstPart.name)
        {
            counters[0]++;
            counters[3]++;
        }
        else if (go.name == ntSecondPart.name)
        {
            counters[2]++;
            counters[1]++;
        }
        for (int i = 0; i < counters.Count; i++)
        {
            if (counters[i] < 0)
            {
                counters[i] = firstPart.Count - 1;
            }
            else if (counters[i] > firstPart.Count - 1)
            {
                counters[i] = 0;
            }
        }
        noFirstPart.sprite = firstPart[counters[0]];
        noSecondPart.sprite = secondPart[counters[1]];
        ntFirstPart.sprite = firstPart[counters[2]];
        ntSecondPart.sprite = secondPart[counters[3]];

        if (ReferenceEquals(go, button))
        {
            CheckWin();
        }
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckMove;
        ClickListener.ObjClicked -= CheckSpites;
    }

    private void CheckWin()
    {
        if (noFirstPart.sprite.name == "OneFP" &&
            noSecondPart.sprite.name == "OneSP" &&
            ntFirstPart.sprite.name == "FourFP" &&
            ntSecondPart.sprite.name == "FourSP")
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }
}