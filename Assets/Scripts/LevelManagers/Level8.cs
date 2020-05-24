using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8 : MonoBehaviour
{
    public List<GameObject> solutionButtons;
    public List<GameObject> solutionFigures;
    public Color figureClickColor;
    private float changeColorDuration = 0.5f;
    private bool isRunning = false;
    private int counter = 0;
    private GameObject lastGo;

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
        if (isRunning && ReferenceEquals(lastGo, go))
        {
            AudioManager.instance.Play("Reset");
            return;
        }

        if (solutionButtons[counter].gameObject == go)
        {
            //change sprite color with self distruct corutine
            //inc counter
            StartCoroutine(ChangeSpriteColor(solutionFigures[counter].gameObject));
            counter++;
        }
        else
        {
            //clear counter
            counter = 0;
            AudioManager.instance.Play("Reset");
        }
        lastGo = go;
        CheckWin();
    }

    private void CheckWin()
    {
        if (counter == solutionButtons.Count)
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }

    private IEnumerator ChangeSpriteColor(GameObject go)
    {
        isRunning = true;
        var sr = go.GetComponent<SpriteRenderer>();
        var originalColor = sr.color;

        float t = 0;

        while (t < changeColorDuration)
        {
            t += Time.deltaTime;

            sr.color = Color.Lerp(figureClickColor, originalColor, t / changeColorDuration);
            yield return null;
        }
        isRunning = false;
    }
}