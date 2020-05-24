using System;
using UnityEngine;

public class SwipeListener : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    public static event Action<SwipeDirection> Swipe = delegate { };

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    CheckSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                CheckSwipe();
            }
        }
    }

    private void CheckSwipe()
    {
        //Check if Vertical swipe
        if (VerticalMove() > SWIPE_THRESHOLD && VerticalMove() > HorizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (HorizontalValMove() > SWIPE_THRESHOLD && HorizontalValMove() > VerticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    private float VerticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    private float HorizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    private void OnSwipeUp()
    {
        //Debug.Log("Swipe UP");
        Swipe(SwipeDirection.Up);
    }

    private void OnSwipeDown()
    {
        //Debug.Log("Swipe Down");
        Swipe(SwipeDirection.Down);
    }

    private void OnSwipeLeft()
    {
        //Debug.Log("Swipe Left");
        Swipe(SwipeDirection.Left);
    }

    private void OnSwipeRight()
    {
        //Debug.Log("Swipe Right");
        Swipe(SwipeDirection.Right);
    }
}