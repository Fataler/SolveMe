using System.Collections.Generic;
using UnityEngine;

public class Level9 : MonoBehaviour
{
    public List<GameObject> solution;
    public List<GameObject> obj;

    public int currentBlock = 0;

    // Start is called before the first frame update
    private void Start()
    {
        ClickListener.ObjClicked += CheckObj;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void CheckObj(GameObject go)
    {
        Debug.Log(go.name);

        if (solution[currentBlock] == go)
        {
            Debug.Log(true);
            obj.Add(go);
            currentBlock++;
        }
        else
        {
            currentBlock = 0;
            obj = new List<GameObject>(10);
            AudioManager.instance.Play(SoundsList.Reset);
        }

        if (solution.Count == obj.Count)
        {
            Debug.Log("Win!");
            GameManager.instance.NextLevel();
        }
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckObj;
    }
}