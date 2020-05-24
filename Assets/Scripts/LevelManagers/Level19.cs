using UnityEngine;

public class Level19 : MonoBehaviour
{
    private Coroutine scaler;
    public GameObject scale;
    public float scaleMin;
    public float scaleMax;

    private void Start()
    {
        ClickListener.ObjClicked += CheckClick;
        ClickListener.ObjReleased += CheckRelease;
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckClick;
        ClickListener.ObjReleased -= CheckRelease;
    }

    private void CheckClick(GameObject go)
    {
        scaler = StartCoroutine(Utilites.ScaleYByTime(scale, 5.7f, 7f));
    }

    private void CheckRelease(GameObject go)
    {
        StopCoroutine(scaler);
        checkWin();
    }

    private void checkWin()
    {
        Vector3 locScale = scale.transform.localScale;
        if (locScale.y >= scaleMin && locScale.y <= scaleMax)
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}