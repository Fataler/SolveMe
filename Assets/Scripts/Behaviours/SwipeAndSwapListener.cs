using System;
using System.Collections;
using UnityEngine;

public class SwipeAndSwapListener : MonoBehaviour
{
    public static event Action Swapped = delegate { };

    public GameObject firstObject;

    public GameObject secondObject;
    public bool isSwaping = false;

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 firstPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D firstCollider = Physics2D.OverlapPoint(firstPosition);
                if (firstCollider != null)
                {
                    firstObject = firstCollider.gameObject;
                }
                //Debug.Log(firstCollider.gameObject.name);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 secondPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D secondColider = Physics2D.OverlapPoint(secondPosition);
                if (secondColider != null)
                {
                    secondObject = secondColider.gameObject;
                }
                //Debug.Log(secondColider.gameObject.name);
                if (!GameObject.ReferenceEquals(firstObject, secondObject) && isSwaping == false)
                {
                    //SwapTwoObjects(firstObject, secondObject);
                    StartCoroutine(SwapTwoObjects(firstObject, secondObject));
                }
            }
        }
    }

    private IEnumerator SwapTwoObjects(GameObject goFirst, GameObject goSecond)
    {
        Vector3 goFirstPos = goFirst.transform.position;
        Vector3 goSecondPos = goSecond.transform.position;
        var firstName = goFirst.name;
        goFirst.name = goSecond.name;
        goSecond.name = firstName;

        float i = 0;
        while (i < 1)
        {
            isSwaping = true;
            i += Time.deltaTime / 0.3f;
            goFirst.transform.position = Vector3.Lerp(goFirstPos, goSecondPos, i);
            goSecond.transform.position = Vector3.Lerp(goSecondPos, goFirstPos, i);
            yield return 0;
        }
        isSwaping = false;
        Swapped();
    }
}