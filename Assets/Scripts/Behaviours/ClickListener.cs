using System;
using UnityEngine;

public class ClickListener : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    /// <summary>
    /// onButtonDown event
    /// </summary>
    public static event Action<GameObject> ObjClicked = delegate { };

    /// <summary>
    /// onButtonUp event
    /// </summary>
    public static event Action<GameObject> ObjReleased = delegate { };

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
#if UNITY_WEBGL || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(ray.origin, ray.direction);
            foreach (RaycastHit2D h in hitInfo)
            {
                //Debug.Log(h.collider.gameObject.name);
                ObjClicked(h.collider.gameObject);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(ray.origin, ray.direction);
            foreach (RaycastHit2D h in hitInfo)
            {
                //Debug.Log(h.collider.gameObject.name);
                ObjReleased(h.collider.gameObject);
            }
        }

#endif
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            var input = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(ray.origin, ray.direction);
            if (hitInfo.Length > 0)
            {
                switch (input.phase)
                {
                    case TouchPhase.Began:

                        //Debug.Log(h.collider.gameObject.name);
                        ObjClicked(hitInfo[0].collider.gameObject);
                        if (clickSound == null)
                        {
                            AudioManager.instance.Play("Click");
                        }
                        else
                        {
                            audioSource.PlayOneShot(clickSound);
                        }

                        break;

                    case TouchPhase.Ended:

                        //Debug.Log(h.collider.gameObject.name);
                        ObjReleased(hitInfo[0].collider.gameObject);

                        break;
                }
            }
        }

#endif
    }
}