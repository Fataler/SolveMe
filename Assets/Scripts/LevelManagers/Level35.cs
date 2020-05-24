using Archon.SwissArmyLib.Coroutines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level35 : MonoBehaviour
{
    public float timer = 0f;
    public GameObject pupil;
    private Animator pupilAnimator;
    public float timeToWin = 15f;
    private int id = 0;

    private void Start()
    {
        TapListener.ScreenTap += CheckTap;
        pupilAnimator = pupil.GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        TapListener.ScreenTap -= CheckTap;
    }

    private void CheckTap(Vector2 obj)
    {
        timer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (AdsManager.instance.adIsRunning)
        {
            timer = 0;
            return;
        }
        timer += Time.deltaTime;

        if (timer > timeToWin)
        {
            Destroy(pupilAnimator);
            if (id == 0)
            {
                id = BetterCoroutines.Start(ScaleYByTime(pupil, 8f, 2f));
            }
        }
    }

    public static IEnumerator ScaleYByTime(GameObject go, float scaleTo, float scaleSpeed)
    {
        Debug.Log("Win");
        Vector3 originalScale = go.transform.localScale;
        Vector3 destinationScale = new Vector3(scaleTo, scaleTo, originalScale.z);
        float currentTime = 0.0f;

        do
        {
            go.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / scaleSpeed);
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        } while (currentTime <= scaleSpeed);
        GameManager.instance.NextLevel();
    }
}