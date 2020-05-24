using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapListener : MonoBehaviour
{
    public static event Action<Vector2> ScreenTap = delegate { };

    public static event Action<Vector2> FingerMoving = delegate { };

    public bool soundEnabled = true;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case (TouchPhase.Began):
                    {
                        if (soundEnabled)
                        {
                            AudioManager.instance.Play("Click");
                        }
                        ScreenTap(position);
                    }
                    break;

                case (TouchPhase.Moved):
                    {
                        FingerMoving(position);
                    }
                    break;

                case (TouchPhase.Ended):
                    {
                    }
                    break;
            }
        }
    }
}