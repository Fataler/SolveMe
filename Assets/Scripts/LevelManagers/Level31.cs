using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level31 : MonoBehaviour
{
    public GameObject[] items;
    public int currentPhase = 0;
    public int touchCounter = 0;
    public Vector2 lastTouch;
    public Vector2 curTouch;

    private void Start()
    {
        TapListener.ScreenTap += CheckTouch;
    }

    private void OnDestroy()
    {
        TapListener.ScreenTap -= CheckTouch;
    }

    private void CheckTouch(Vector2 touch)
    {
        curTouch = touch;
        touchCounter++;

        switch (currentPhase)
        {
            case 0:
                lastTouch = curTouch;
                NextPhase();
                break;

            case 1:
                if (curTouch.y < lastTouch.y)
                {
                    lastTouch = curTouch;

                    if (touchCounter == 1)
                    {
                        NextPhase();
                    }
                }
                else
                {
                    ResetState();
                }
                break;

            case 2:
                if (curTouch.y > lastTouch.y)
                {
                    lastTouch = curTouch;

                    if (touchCounter == 2)
                    {
                        NextPhase();
                    }
                }
                else
                {
                    ResetState();
                }
                break;

            case 3:
                if (curTouch.x > lastTouch.x)
                {
                    lastTouch = curTouch;

                    if (touchCounter == 2)
                    {
                        NextPhase();
                    }
                }
                else
                {
                    ResetState();
                }
                break;

            case 4:
                if (curTouch.x < lastTouch.x)
                {
                    lastTouch = curTouch;

                    if (touchCounter == 3)
                    {
                        NextPhase();
                    }
                }
                else
                {
                    ResetState();
                }
                break;

            case 5:
                if (curTouch.y < lastTouch.y)
                {
                    lastTouch = curTouch;

                    if (touchCounter == 2)
                    {
                        NextPhase();
                        Debug.Log("Win");
                        GameManager.instance.NextLevel();
                    }
                }
                else
                {
                    ResetState();
                }
                break;
        }
    }

    private void ResetState()
    {
        touchCounter = 0;
        currentPhase = 0;
        Array.ForEach(items, item => item.SetActive(true));
    }

    private void NextPhase()
    {
        items[currentPhase].SetActive(false);
        currentPhase++;
        touchCounter = 0;
    }
}