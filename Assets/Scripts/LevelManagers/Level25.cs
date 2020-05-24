using System;
using UnityEngine;
using UnityEngine.UI;

public class Level25 : MonoBehaviour
{
    public Color clickedColor;
    public Color defaultColor;
    public AudioClip clip;
    public Sprite snakeSprite;

    private GameObject[] letters;
    private string command;
    private AudioSource audioSource;
    private GameObject player;
    private GameObject winPoint;
    private GameObject[] skeletons;

    private enum Commands
    {
        DOWN,
        LEFT,
        RIGHT,
        DOWEN
    }

    private void Start()
    {
        letters = GameObject.FindGameObjectsWithTag("Letters");
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        winPoint = GameObject.FindGameObjectWithTag("WinPoint");
        skeletons = GameObject.FindGameObjectsWithTag("Object");
    }

    public void OnLetterClick(UnityEngine.Object obj)
    {
        GameObject clickedObj = (GameObject)obj;
        Text text = clickedObj.GetComponent<Text>();
        if (text.color != clickedColor)
        {
            text.color = clickedColor;
            command += text.text;
        }
        Debug.Log(command);
        CheckCommand();
    }

    private void CheckCommand()
    {
        var playerMove = player.GetComponent<MovableObject>();
        switch (command)
        {
            case "DOWEN":
                audioSource.PlayOneShot(clip);

                Array.ForEach(skeletons, x =>
                {
                    var sprite = x.GetComponent<SpriteRenderer>();
                    sprite.sprite = snakeSprite;
                });
                ResetText();
                break;

            case "DOWN":
                playerMove.Move(Vector2.down);
                ResetText();
                break;

            case "LEFT":
                playerMove.Move(Vector2.left);
                ResetText();
                break;

            case "UP":
                playerMove.Move(Vector2.up);
                ResetText();
                break;

            case "RIGHT":
                playerMove.Move(Vector2.right);
                ResetText();
                break;

            case "WIN":
                playerMove.transform.position = winPoint.transform.position;
                ResetText();
                break;
        }
        CheckWin();
    }

    public void ResetText()
    {
        foreach (var letter in letters)
        {
            Text text = letter.GetComponent<Text>();
            text.color = defaultColor;
        }
        command = "";
    }

    private void CheckWin()
    {
        var tiles = GameObject.FindGameObjectsWithTag("Tile");
        bool outOfField = Array.Exists(tiles, tile => tile.transform.position == player.transform.position);
        if (!Utilites.IsTargetVisible(Camera.main, player) || !outOfField)
        {
            Utilites.RestartScene();
        }

        Array.ForEach(skeletons, skeleton =>
        {
            if (skeleton.transform.position == player.transform.position)
            {
                Utilites.RestartScene();
            }
        });

        if (player.transform.position == winPoint.transform.position)
        {
            Debug.Log("Win");
            GameManager.instance.NextLevel();
        }
    }
}