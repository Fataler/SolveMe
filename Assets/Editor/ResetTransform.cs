using UnityEditor;
using UnityEngine;

// reset transform position, rotation and scale

namespace UnityLibrary
{
    public class ResetTransform : ScriptableObject
    {
        [MenuItem("GameObject/Reset Transform #r")]
        static public void MoveSceneViewCamera()
        {
            var go = Selection.GetTransforms(SelectionMode.TopLevel);
            if (go != null)
            {
                foreach (var item in go)
                {
                    item.transform.position = Vector3.zero;
                    item.transform.rotation = Quaternion.identity;
                    item.transform.localScale = Vector3.one;
                }
            }
        }
    }
}