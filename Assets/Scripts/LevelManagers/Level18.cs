using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Level18 : MonoBehaviour
{
    public ClickBehaviour[] buttons;
    public Text decimalVal;
    public GameObject submit;
    private string decimalValue = "0";

    // Start is called before the first frame update
    private void Start()
    {
        //buttons = UnityEngine.Object.FindObjectsOfType<ClickBehaviour>();
        ClickListener.ObjClicked += CheckMove;
    }

    private void OnDestroy()
    {
        ClickListener.ObjClicked -= CheckMove;
    }

    private void CheckMove(GameObject go)
    {
        if (ReferenceEquals(go, submit))
        {
            CheckWin(decimalValue);
        }
        else
        {
            var script = go.GetComponent<ClickBehaviour>();
            script.hold = !script.hold;
            StringBuilder binaryString = new StringBuilder();
            foreach (var item in buttons)
            {
                switch (!item.hold)
                {
                    case true:
                        binaryString.Append("1");
                        break;

                    case false:
                        binaryString.Append("0");
                        break;
                }
            }
            //Debug.Log(binaryString.ToString());
            decimalValue = Convert.ToInt32(binaryString.ToString(), 2).ToString();
            decimalVal.text = decimalValue;
        }
    }

    public static void CheckWin(string value)
    {
        if (value == "18")
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }
}