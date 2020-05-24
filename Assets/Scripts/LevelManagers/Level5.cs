using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour
{
    public GameObject objs;
    private int check = 0;

    [SerializeField]
    private List<GameObject> playerobjs;

    private void Start()
    {
        SwipeAndSwapListener.Swapped += CheckForWin;
        playerobjs = new List<GameObject>();
    }

    private void OnDestroy()
    {
        SwipeAndSwapListener.Swapped -= CheckForWin;
    }

    public void CheckForWin()
    {
        check = 0;
        playerobjs.Clear();
        for (int i = 0; i < objs.transform.childCount; i++)
        {
            playerobjs.Add(objs.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < playerobjs.Count; i++)
        {
            if (playerobjs[i].name == $"Wave_{i + 1}")
            {
                check++;
            }
        }
        if (check > 0 && check == playerobjs.Count)
        {
            GameManager.instance.NextLevel();
        }
        //playerobjs.ForEach(Debug.Log);
    }
}