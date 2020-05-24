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
    private List<string> levelTips;
    private int tipCounter;

    private void Start()
    {
        Invoke("ShowHintButton", 10f);
        levelTips = GameManager.instance.GetTips(GameManager.instance.currentLevel);
        hintText.text = "";
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
        if (levelTips != null)
        {
            hint.gameObject.SetActive(true);
            if (tipCounter < levelTips.Count)
            {
                if (tipCounter > 0)
                {
                    hintText.text += "\n";
                }
                hintText.text += levelTips[tipCounter];
                hintText.text.Trim();
                tipCounter++;
            }
            else
            {
                hintText.text = string.Join("\n", levelTips);
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
}