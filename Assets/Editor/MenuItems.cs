using UnityEditor;
using UnityEngine;

public class MenuItems : MonoBehaviour
{
    [MenuItem("GameObject/Listeners/Create ClickListener", false, 0)]
    private static void DoCreateClickListener()
    {
        GameObject go = (GameObject)Instantiate(Resources.Load("ClickListener"));
        //go.AddComponent<ClickListener>();
        go.name = "ClickListener";
        go.transform.position = Vector3.zero;
    }
}