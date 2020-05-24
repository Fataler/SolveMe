using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level33 : MonoBehaviour
{
    private float baseAngle = 0;
    public float totalAngles;
    public float valueToWin;
    private float lastangle = 0;
    public Transform bg;

    private void OnMouseDrag()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;

        var ang = Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg - baseAngle;
        if (lastangle != ang)
        {
            totalAngles += Mathf.Abs(ang - lastangle);
            if (totalAngles > valueToWin)
            {
                if (bg.position.x >= 0)
                {
                    bg.position = Vector3.zero;
                    Debug.Log("Win");
                    GameManager.instance.NextLevel();
                }
                else
                {
                    bg.Translate(new Vector3(0.1f, 0, 0));
                }
            }
        }
        transform.rotation = Quaternion.AngleAxis(-ang, Vector3.forward);
        lastangle = ang;
    }

    private void OnMouseDown()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        baseAngle = Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg;
    }

    private void OnMouseUp()
    {
        totalAngles = 0;
        lastangle = 0;
    }
}