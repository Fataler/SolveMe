using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private bool _music;
    public GameObject cross;
    public Image resetButton;
    public Color resetColor;
    private Color _normalColor;
    public Text lvlText;

    private void Start()
    {
        _normalColor = resetButton.color;
        _music = AudioManager.instance.isEnabled;
        if (_music)
        {
            cross.SetActive(false);
        }
        else
        {
            cross.SetActive(true);
        }
    }

    public void ToggleMusic()
    {
        _music = !_music;
        AudioManager.instance.ToggleMusic();
        cross.SetActive(!cross.activeSelf);
    }

    public void ResetProgress()
    {
        if (resetButton.color == resetColor)
        {
            GameManager.instance.lastOpenedLevel = 1;
            PlayerPrefs.SetInt("last_level", 1);
            resetButton.color = _normalColor;
            lvlText.text = "1";
        }
        else
        {
            resetButton.color = resetColor;
            Invoke("Unreset", 2f);
        }
    }

    private void Unreset()
    {
        resetButton.color = _normalColor;
    }
}