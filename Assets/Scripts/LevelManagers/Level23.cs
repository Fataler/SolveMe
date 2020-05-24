using UnityEngine;

public class Level23 : MonoBehaviour
{
    public int knockCount = 4;

    //public GameObject frog;
    public MovableObject frog;

    public GameObject frogSprite;
    public AudioClip wallKnock;
    public AudioClip fakeWallKnock;
    private AudioSource audioSource;
    private GameObject fakeWall;
    public Vector3 winPos;
    public Vector3 fakeWallPos;

    private void Start()
    {
        SwipeListener.Swipe += CheckSwipe;
        audioSource = GetComponent<AudioSource>();
        fakeWall = GameObject.Find("Fake_wall");
        fakeWallPos = fakeWall.transform.position + Vector3.left;
        winPos = fakeWall.transform.position + Vector3.right;
    }

    private void CheckSwipe(SwipeListener.SwipeDirection dir)
    {
        RotateGameObject(dir, frogSprite);

        if (!frog.Move(DirectionVector(dir)))
        {
            if (frog.transform.position == fakeWallPos)
            {
                knockCount--;
                audioSource.PlayOneShot(fakeWallKnock);
            }
            else
            {
                audioSource.PlayOneShot(wallKnock);
            }
        }

        if (knockCount < 0)
        {
            fakeWall.SetActive(false);
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if (frog.transform.position == winPos)
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }

    private void OnDestroy()
    {
        SwipeListener.Swipe -= CheckSwipe;
    }

    // Update is called once per frame
    private void RotateGameObject(SwipeListener.SwipeDirection dir, GameObject go)
    {
        switch (dir)
        {
            case SwipeListener.SwipeDirection.Down:
                go.transform.localRotation = Quaternion.AngleAxis(0f, Vector3.forward);
                break;

            case SwipeListener.SwipeDirection.Right:
                go.transform.localRotation = Quaternion.AngleAxis(90f, Vector3.forward);
                break;

            case SwipeListener.SwipeDirection.Left:
                go.transform.localRotation = Quaternion.AngleAxis(-90f, Vector3.forward);
                break;

            case SwipeListener.SwipeDirection.Up:
                go.transform.localRotation = Quaternion.AngleAxis(180f, Vector3.forward);
                break;
        }
    }

    private Vector2 DirectionVector(SwipeListener.SwipeDirection dir)
    {
        var dirVector = new Vector2(0, 0);
        switch (dir)
        {
            case SwipeListener.SwipeDirection.Down:
                dirVector = Vector2.down;
                break;

            case SwipeListener.SwipeDirection.Right:
                dirVector = Vector2.right;
                break;

            case SwipeListener.SwipeDirection.Left:
                dirVector = Vector2.left;
                break;

            case SwipeListener.SwipeDirection.Up:
                dirVector = Vector2.up;
                break;
        }
        return dirVector;
    }
}