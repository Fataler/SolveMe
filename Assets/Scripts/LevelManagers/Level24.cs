using System.Collections.Generic;
using UnityEngine;

public class Level24 : MonoBehaviour
{
    private GameObject[] pyramids;
    private GameObject fromPyramid;
    private GameObject toPyramid;

    private void Start()
    {
        pyramids = GameObject.FindGameObjectsWithTag("Object");
        foreach (var pyramid in pyramids)
        {
            var items = Utilites.GetAllChildren(pyramid);
            var pyramidScript = pyramid.GetComponent<Pyramid>();
            pyramidScript.items = new Stack<GameObject>();
            for (int i = items.Count - 1; i >= 0; i--)
            {
                pyramidScript.items.Push(items[i]);
            }
        }
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Vector2 fromPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    Collider2D fromCollider = Physics2D.OverlapPoint(fromPosition);
                    if (fromCollider != null)
                    {
                        fromPyramid = fromCollider.gameObject;
                    }
                    break;

                case TouchPhase.Ended:
                    Vector2 toPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    Collider2D toColider = Physics2D.OverlapPoint(toPosition);
                    if (toColider != null)
                    {
                        toPyramid = toColider.gameObject;
                    }
                    if (!GameObject.ReferenceEquals(fromPyramid, toPyramid))
                    {
                        DoMove(fromPyramid, toPyramid);
                    }
                    break;
            }
        }
    }

    private void DoMove(GameObject fromGo, GameObject toGo)
    {
        var fromScript = fromGo.GetComponent<Pyramid>();
        var fromStack = fromScript.items;
        var toScript = toGo.GetComponent<Pyramid>();
        var toStack = toScript.items;
        if (toStack.Count < 4)
        {
            var fromItem = fromStack.Pop();
            var preItem = toStack.Peek();
            Vector3 newpos = preItem.transform.position + new Vector3(0, 0.750f, 0);
            fromItem.transform.SetParent(preItem.transform.parent);
            fromItem.transform.position = newpos;
            toStack.Push(fromItem);
            /*Debug.Log(String.Join(", ", fromStack));
            Debug.Log(String.Join(", ", toStack));*/

            CheckWin();
        }
    }

    private void CheckWin()
    {
        bool win = true;
        foreach (var pyramid in pyramids)
        {
            var currPyramid = pyramid.GetComponent<Pyramid>();
            win &= currPyramid.CheckWin();
        }
        if (win)
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }
}