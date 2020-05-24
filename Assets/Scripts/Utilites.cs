using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main utility static class with helper methods.
/// </summary>
public static class Utilites
{
    /// <summary>
    /// Reset all item's state to "unhold"
    /// </summary>
    /// <param name="list"></param>
    public static void ResetButtonsState(List<ClickBehaviour> list)
    {
        foreach (var item in list)
        {
            item.alreadyClicked = false;
            item.hold = false;
        }
    }

    /// <summary>
    /// Reset state of GameObject array with buttons
    /// </summary>
    /// <param name="array"></param>
    public static void ResetButtonsState(GameObject[] array)
    {
        foreach (var item in array)
        {
            var script = item.GetComponent<ClickBehaviour>();
            script.alreadyClicked = false;
            script.hold = false;
        }
    }

    public static List<Vector3> GetLocalNeighbourPositions(GameObject go)
    {
        Vector3 currentPos = go.transform.localPosition;
        List<Vector3> neighbourPositions = new List<Vector3>
        {
            currentPos + Vector3.left,
            currentPos + Vector3.right,
            currentPos + Vector3.up,
            currentPos + Vector3.down
        };
        return neighbourPositions;
    }

    /// <summary>
    /// Checks for win condition and returns true if all items are held.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool AllItemsHolded(List<ClickBehaviour> list)
    {
        int holded = 0;
        foreach (var item in list)
        {
            if (item.hold)
                holded++;
        }
        if (holded == list.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool AllItemsHolded(List<GameObject> list)
    {
        var listClick = new List<ClickBehaviour>();
        list.ForEach(x =>
        {
            listClick.Add(x.GetComponent<ClickBehaviour>());
        });
        if (AllItemsHolded(listClick))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static IEnumerator ScaleChanger(GameObject go, float scaleSpeed)
    {
        Vector3 originalScale = new Vector3(0f, 0f, 0f);
        Vector3 destinationScale = new Vector3(1.3f, 1.3f, 1.3f);

        float currentTime = 0.0f;

        do
        {
            go.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / scaleSpeed);
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        } while (currentTime <= scaleSpeed);
        go.transform.localScale = originalScale;
    }

    public static IEnumerator ScaleYByTime(GameObject go, float scaleTo, float scaleSpeed)
    {
        Vector3 originalScale = new Vector3(5f, 0f, 1f);
        Vector3 destinationScale = new Vector3(originalScale.x, scaleTo, originalScale.z);
        float currentTime = 0.0f;

        do
        {
            go.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / scaleSpeed);
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        } while (currentTime <= scaleSpeed);
        go.transform.localScale = originalScale;
    }

    public static List<GameObject> GetAllChildren(GameObject go)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (Transform child in go.transform)
        {
            list.Add(child.gameObject);
        }
        return list;
    }

    public static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static bool IsTargetVisible(Camera c, GameObject go)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = go.transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }

    public static List<GameObject> GetListOf(string tag)
    {
        var list = new List<GameObject>();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(tag))
        {
            list.Add(item);
        }
        if (list != null)
        {
            return list;
        }
        else
        {
            return null;
        }
    }

    public static IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();
        //Texture2D tex = new Texture2D(Screen.width, Screen.height);
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        byte[] _bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/1.png", _bytes);
        // do something with texture
        // cleanup Object.Destroy(texture);
    }
}