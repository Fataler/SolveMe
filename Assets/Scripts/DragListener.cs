using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragListener : MonoBehaviour
{
    public static event Action<GameObject> ObjMoving = delegate { };

    private GameObject _obj;
    private float _startPosX;
    private float _startPosY;

    private Camera _cam;

    [SerializeField]
    private float _maxX;

    [SerializeField]
    private float _maxY;

    private void Start()
    {
        _cam = Camera.main;
        _maxY = _cam.orthographicSize - 1f;
        _maxX = _cam.orthographicSize * _cam.aspect;
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began)
            {
                Collider2D collider = Physics2D.OverlapPoint(position);
                if (collider != null)
                {
                    _obj = collider.gameObject;
                    _startPosX = position.x - _obj.transform.localPosition.x;
                    _startPosY = position.y - _obj.transform.localPosition.y;
                }
                //Debug.Log(firstCollider.gameObject.name);
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (_obj != null)
                {
                    Vector3 movepos = new Vector3(Mathf.Round((position.x - _startPosX) * 10) / 10,
                        Mathf.Round((position.y - _startPosY) * 10) / 10, 0);

                    _obj.transform.localPosition = movepos;
                    _obj.transform.localPosition = new Vector3(Mathf.Clamp(_obj.transform.localPosition.x, -_maxX, _maxX),
                Mathf.Clamp(_obj.transform.localPosition.y, -_maxY, _maxY));
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (_obj != null)
                {
                    ObjMoving(_obj);
                }
                _obj = null;
            }
        }
    }
}