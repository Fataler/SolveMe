using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level20 : MonoBehaviour
{
    public GameObject objects;
    public List<bool> objectsList;
    public GameObject solvedObjects;
    public List<bool> solvedObjectsList;

    private void Start()
    {
        ClickListener.ObjClicked += CheckClick;

        Utilites.GetAllChildren(solvedObjects).ForEach(x =>
        {
            var item = x.GetComponent<ClickBehaviour>();
            solvedObjectsList.Add(item.hold);
        });
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckClick;
    }

    private void CheckClick(GameObject go)
    {
        var clickBehaviour = go.GetComponent<ClickBehaviour>();
        clickBehaviour.hold = !clickBehaviour.hold;
        objectsList.Clear();
        Utilites.GetAllChildren(objects).ForEach(x =>
        {
            var item = x.GetComponent<ClickBehaviour>();
            objectsList.Add(item.hold);
        });
        checkwin();
    }

    private void checkwin()
    {
        if (objectsList.SequenceEqual(solvedObjectsList))
        {
            Debug.Log("win");
            GameManager.instance.NextLevel();
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}