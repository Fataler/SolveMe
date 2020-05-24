using UnityEngine;

public class Level13 : MonoBehaviour
{
    public float firstItemChangeSpeed = 1f;
    public float secondItemChangeSpeed = 2f;
    public float thirdItemChangeSpeed = 1.5f;
    public float forthItemChangeSpeed = 0.75f;

    public GameObject firstGameObject = null;
    public GameObject secondGameObject = null;
    public GameObject thirdGameObject = null;
    public GameObject forthGameObject = null;

    public GameObject[] items;

    private int checker = 0;

    private void Start()
    {
        ClickListener.ObjClicked += CheckClick;
        items = GameObject.FindGameObjectsWithTag("Object");
    }

    private void CheckClick(GameObject go)
    {
        var child = go.transform.GetChild(0).gameObject;
        if (GameObject.ReferenceEquals(go, firstGameObject))
        {
            StartCoroutine(Utilites.ScaleChanger(child, firstItemChangeSpeed));
        }
        else if (GameObject.ReferenceEquals(go, secondGameObject))
        {
            StartCoroutine(Utilites.ScaleChanger(child, secondItemChangeSpeed));
        }
        else if (GameObject.ReferenceEquals(go, thirdGameObject))
        {
            StartCoroutine(Utilites.ScaleChanger(child, thirdItemChangeSpeed));
        }
        else if (GameObject.ReferenceEquals(go, forthGameObject))
        {
            StartCoroutine(Utilites.ScaleChanger(child, forthItemChangeSpeed));
        }
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckClick;
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var item in items)
        {
            var scaleX = item.transform.localScale.x;
            if (scaleX < 1 && scaleX > 0.85f)
            {
                checker++;
            }
        }
        if (checker == 4)
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
        checker = 0;
    }
}