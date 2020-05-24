using System.Collections.Generic;
using UnityEngine;

public class Level10 : MonoBehaviour
{
    public List<Sprite> dices;
    public GameObject dicePlaceholder;

    public List<ListOfGameObjects> solutionList;
    private SpriteRenderer sr;
    public int counter = 0;
    public int unpassed = 0;
    public int passed = 0;

    private void Start()
    {
        ClickListener.ObjClicked += CheckMove;
        sr = dicePlaceholder.GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckMove;
    }

    private void CheckMove(GameObject obj)
    {
        if (!solutionList[counter].item.Contains(obj))
        {
            unpassed++;
        }
        else
        {
            for (int i = 0; i <= counter; i++)
            {
                if (GameObject.ReferenceEquals(solutionList[counter].item[i], obj))
                {
                    var script = obj.GetComponent<ClickBehaviour>();
                    if (script.alreadyClicked == false)
                    {
                        script.alreadyClicked = true;
                        passed++;
                    }
                    else
                    {
                        unpassed++;
                    }
                }
            }
        }

        if (unpassed > 0)
        {
            //bad
            foreach (GameObject item in solutionList[counter].item)
            {
                var script = item.GetComponent<ClickBehaviour>();
                script.alreadyClicked = false;
            }
            counter = 0;
            passed = 0;
        }
        else if (passed == counter + 1)
        {
            //good
            foreach (GameObject item in solutionList[counter].item)
            {
                var script = item.GetComponent<ClickBehaviour>();
                script.alreadyClicked = false;
            }

            counter++;
            passed = 0;
        }
        unpassed = 0;
        if (counter < solutionList.Count)
        {
            sr.sprite = dices[counter];
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if (counter == solutionList.Count)
        {
            Debug.Log("win");
            GameManager.instance.NextLevel();
        }
    }

    [System.Serializable]
    public class ListOfGameObjects
    {
        public List<GameObject> item;
    }
}