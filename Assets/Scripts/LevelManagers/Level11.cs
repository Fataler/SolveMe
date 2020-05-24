using System.Collections.Generic;
using UnityEngine;

public class Level11 : MonoBehaviour
{
    public Transform arrows;

    public Transform balls;
    public MovableObject arrowUp;
    public MovableObject arrowDown;
    public MovableObject arrowRight;

    public List<MovableObject> objects = new List<MovableObject>();
    public List<MovableObject> arrowsList = new List<MovableObject>();
    public List<MovableObject> ballsList = new List<MovableObject>();
    public List<Vector3> initPositions = new List<Vector3>();

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Object");
        foreach (var obj in gameObjects)
        {
            MovableObject movableObject = obj.GetComponent<MovableObject>();
            objects.Add(movableObject);
            initPositions.Add(obj.transform.position);
            if (obj.transform.IsChildOf(arrows))
            {
                arrowsList.Add(movableObject);
            }
            if (obj.transform.IsChildOf(balls))
            {
                ballsList.Add(movableObject);
            }
        }

        SwipeListener.Swipe += OnSwipe;
    }

    private void CheckForRestart()
    {
        foreach (var item in arrowsList)
        {
            Vector3 pos = item.transform.position;
            if (pos.x <= minX || pos.x >= maxX || pos.y <= minY || pos.y >= maxY)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    //objects[i].transform.position = initPositions[i];
                    Utilites.RestartScene();
                }
            }
        }
    }

    private void OnDestroy()
    {
        SwipeListener.Swipe -= OnSwipe;
    }

    private void OnSwipe(SwipeListener.SwipeDirection swipeDir)
    {
        switch (swipeDir)
        {
            case (SwipeListener.SwipeDirection.Up):
                arrowUp.Move(new Vector2(0, 1));
                break;

            case (SwipeListener.SwipeDirection.Down):
                arrowDown.Move(new Vector2(0, -1));
                break;

            case (SwipeListener.SwipeDirection.Right):
                arrowRight.Move(new Vector2(1, 0));
                break;
        }
        CheckForRestart();
        CheckForWin();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void CheckForWin()
    {
        int check = 0;
        foreach (var item in ballsList)
        {
            Vector3 pos = item.transform.position;
            if (pos.x <= minX || pos.x >= maxX || pos.y <= minY || pos.y >= maxY)
            {
                check++;
                item.transform.position += Vector3.one * 5;
            }
        }
        if (check == ballsList.Count)
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }
}