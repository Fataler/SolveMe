using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level39 : MonoBehaviour
{
    public int speed = 5;
    public GameObject arrow;
    public GameObject button;

    // Start is called before the first frame update
    private void Start()
    {
        ClickListener.ObjClicked += CheckClick;
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckClick;
    }

    private void CheckClick(GameObject go)
    {
        if (ReferenceEquals(go, button))
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        var input = Input.acceleration;
        //Debug.Log($"x {input.x} y {input.y}");
        if (input.normalized.y > 0.9f)
        {
            arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, Vector3.up * 10, speed * Time.deltaTime);
            Debug.Log("moving");
        }
    }
}