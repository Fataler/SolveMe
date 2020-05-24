using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level30 : MonoBehaviour
{
    public List<GameObject> solution;
    public List<GameObject> curTiles;

    private int counter = 0;

    // Start is called before the first frame update
    private void Start()
    {
        DragListener.ObjMoving += CheckMove;
        counter = solution.Count - 1;
    }

    private void CheckMove(GameObject go)
    {
        foreach (var item in solution)
        {
            if (go.name == item.name)
            {
                var sprite = go.GetComponent<SpriteRenderer>();
                sprite.sortingOrder = 4;
                var itemClosePos = new Vector3(Mathf.Round(item.transform.localPosition.x * 10) / 10,
                    Mathf.Round(item.transform.localPosition.y * 10) / 10);
                //Debug.Log(Vector3.Distance(go.transform.localPosition, itemClosePos));
                if (Vector3.Distance(go.transform.localPosition, itemClosePos) < 0.3f)
                {
                    go.transform.localPosition = item.transform.localPosition;
                    sprite.sortingOrder = 2;
                    var goCol = go.GetComponent<BoxCollider2D>();
                    Destroy(goCol);
                    counter--;
                    CheckWin();
                }
            }
        }
    }

    private void CheckWin()
    {
        if (counter == 0)
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }

    private void OnDestroy()
    {
        DragListener.ObjMoving -= CheckMove;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}