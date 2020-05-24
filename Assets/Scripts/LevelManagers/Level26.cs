using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level26 : MonoBehaviour
{
    public List<GameObject> coins;
    public List<GameObject> indicators;
    private float RotateDuration = 0.3f;
    private static bool isRunning;

    public Color indicatorColor;
    public Color defaultColor;
    public GameObject indicatorsPanel;

    // Start is called before the first frame update
    private void Start()
    {
        ClickListener.ObjClicked += CheckClick;
        coins = Utilites.GetListOf("WinPoint");
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckClick;
    }

    private void CheckClick(GameObject go)
    {
        if (go.CompareTag("WinPoint"))
        {
            /*if (!indicatorsPanel.activeSelf)
            {
                indicatorsPanel.SetActive(true);
                indicators = Utils.GetListOf("Indicator");
            }*/
            var script = go.GetComponent<ClickBehaviour>();
            script.ToggleHold();

            //indicators stuff
            /* for (int i = 0; i < coins.Count; i++)
             {
                 if (ReferenceEquals(coins[i], go))
                 {
                     var currIndicator = indicators[i];
                     var renderer = currIndicator.GetComponent<SpriteRenderer>();
                     if (script.hold)
                     {
                         renderer.color = indicatorColor;
                     }
                     else
                     {
                         renderer.color = defaultColor;
                     }
                 }
             }*/
        }
        else if (go.CompareTag("Object"))
        {
            if (!isRunning)
            {
                StartCoroutine(Rotate(go.transform.parent.gameObject, 90f, RotateDuration, Vector3.forward));
            }
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if (Utilites.AllItemsHolded(coins))
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }

    public static IEnumerator Rotate(GameObject go, float angle, float duration, Vector3 axis)
    {
        isRunning = true;
        Quaternion from = go.transform.rotation;
        Quaternion to = go.transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            go.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        go.transform.rotation = to;
        isRunning = false;
    }
}