using UnityEngine;

public class Level12 : MonoBehaviour
{
    public TilePlayer[] players;
    public Vector3 posToWinG;
    public Vector3 posToWinW;
    private int horizontal;
    private int vertical;

    private void Start()
    {
        SwipeListener.Swipe += CheckMove;
        posToWinG = GameObject.Find("WinPosG").transform.position;
        posToWinW = GameObject.Find("WinPosW").transform.position;
    }

    private void CheckMove(SwipeListener.SwipeDirection swipeDirection)
    {
        Vector2 moveInput = new Vector2(0, 0);
        switch (swipeDirection)
        {
            case SwipeListener.SwipeDirection.Up:
                moveInput = new Vector2(0, 1);
                break;

            case SwipeListener.SwipeDirection.Down:
                moveInput = new Vector2(0, -1);
                break;

            case SwipeListener.SwipeDirection.Left:
                moveInput = new Vector2(-1, 0);
                break;

            case SwipeListener.SwipeDirection.Right:
                moveInput = new Vector2(1, 0);
                break;
        }
        if (moveInput.sqrMagnitude > 0.5)
        {
            foreach (var player in players)
            {
                player.Move(moveInput);
            }
            foreach (var player in players)
            {
                if (player.blockedByTile)
                {
                    player.Move(moveInput);
                    player.blockedByTile = false;
                }
            }
        }
    }

    private void OnDestroy()
    {
        SwipeListener.Swipe -= CheckMove;
    }

    // Update is called once per frame
    private void Update()
    {
        //GetInput(horizontal, vertical);

        CheckWin();
    }

    public void CheckWin()
    {
        if (players[1].transform.position == posToWinG && players[2].transform.position == posToWinW)
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }
}