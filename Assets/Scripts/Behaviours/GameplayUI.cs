using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System;

public class GameplayUI : MonoBehaviour
{
    public Text hintText;
    public GameObject fadeScr;
    public GameObject hint;
    public Button hintButton;
    public Button skipButton;

    private List<string> _levelTips;
    private int _tipCounter;

    private void Start()
    {
        Invoke("ShowHintButton", 10f);
        _levelTips = GameManager.instance.GetTips(GameManager.instance.currentLevel);
        hintText.text = "";

#if UNITY_EDITOR || UNITY_WEBGL
        //enbled only in web and editor
        skipButton.gameObject.SetActive(true);
#endif
    }

    public void ShowHintButton()
    {
        hintButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackPressed();
        }
    }

    public void BackPressed()
    {
        fadeScr.SetActive(!fadeScr.activeSelf);
    }

    public void HintPressed()
    {
        Analytics.CustomEvent("hint_click", new Dictionary<string, object>
        {
                { "level_index",GameManager.instance.currentLevel },
                {"timer",GameManager.instance.timer }
        });
        AdsManager.instance.ShowAd();
    }

    public void HideHint()
    {
        hint.gameObject.SetActive(false);
    }

    public void ExitYes()
    {
        SceneManager.LoadScene(0);
    }

    /// ads

    public void AdsDidFinish()
    {
        Debug.Log("nice");
        if (_levelTips != null)
        {
            hint.gameObject.SetActive(true);
            if (_tipCounter < _levelTips.Count)
            {
                if (_tipCounter > 0)
                {
                    hintText.text += "\n";
                }
                hintText.text += _levelTips[_tipCounter];
                hintText.text.Trim();
                _tipCounter++;
            }
            else
            {
                hintText.text = string.Join("\n", _levelTips);
            }
            Invoke("HideHint", 20f);
        }
    }

    public void AdsDidError()
    {
        Debug.Log("Sad");
        hint.gameObject.SetActive(true);
        hintText.text = "Connection Error";

        Invoke("HideHint", 10f);
    }

    public void SkipLevel()
    {
        GameManager.instance.NextLevel();
    }
}