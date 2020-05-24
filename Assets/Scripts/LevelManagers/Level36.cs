using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Level36 : MonoBehaviour
{
    public float timer = 0f;
    public Vector2 position;
    private List<Record> leftLines = new List<Record>();
    private List<Record> rightLines = new List<Record>();
    private List<Record> distanseX = new List<Record>();
    private List<Record> distanseY = new List<Record>();
    public GameObject left;
    public GameObject right;
    public GameObject x;
    public GameObject y;

    private float offset = 8f;
    private float distanceOffset = 0.2f;
    private bool win = false;

    public struct Record
    {
        public GameObject go;
        public Vector3 initPosition;
    }

    private void OnDestroy()
    {
        TapListener.FingerMoving -= CheckTouch;
    }

    private void Start()
    {
        TapListener.FingerMoving += CheckTouch;
        leftLines = getItems(left);
        rightLines = getItems(right);
        distanseX = getItems(x);
        distanseY = getItems(y);
    }

    private List<Record> getItems(GameObject go)
    {
        var parent = Utilites.GetAllChildren(go);
        var result = new List<Record>();
        foreach (var item in parent)
        {
            Record i = new Record
            {
                go = item,
                initPosition = item.transform.position
            };
            result.Add(i);
            //move out
            item.transform.position = new Vector3(item.transform.position.x + offset, item.transform.position.y, item.transform.position.z);
        }
        return result;
    }

    private void CheckTouch(Vector2 vec)
    {
        var distY = Math.Abs(vec.y - position.y);
        var distX = Math.Abs(vec.x - position.x);
        var distance = Vector2.Distance(position, vec);

        //left
        foreach (var item in leftLines)
        {
            var initPos = item.initPosition;
            item.go.transform.position = new Vector3(initPos.x - distance + distanceOffset, initPos.y, initPos.z);
        }
        //right
        foreach (var item in rightLines)
        {
            var initPos = item.initPosition;
            item.go.transform.position = new Vector3(initPos.x + distance - distanceOffset, initPos.y, initPos.z);
        }
        //x
        foreach (var item in distanseX)
        {
            var initPos = item.initPosition;
            item.go.transform.position = new Vector3(initPos.x + distX - distanceOffset, initPos.y, initPos.z);
        }
        //y
        foreach (var item in distanseY)
        {
            var initPos = item.initPosition;
            item.go.transform.position = new Vector3(initPos.x - distY + distanceOffset, initPos.y, initPos.z);
        }
        if (distance < distanceOffset)
        {
            win = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (win)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                Debug.Log("WIN");
                GameManager.instance.NextLevel();
            }
        }
    }
}