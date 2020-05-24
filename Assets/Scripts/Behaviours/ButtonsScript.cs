using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    private const float RepeatRate = 0.2f;
    public Text LevelText;
    private int lvlToLoad;
    public GameObject exitScr;

    private void Start()
    {
        lvlToLoad = GameManager.instance.lastOpenedLevel;
        LevelText.text = lvlToLoad.ToString();
    }

    private void Update()
    {
        if (lvlToLoad < 1) lvlToLoad = 1;
        if (lvlToLoad > GameManager.instance.lastOpenedLevel)
            lvlToLoad = GameManager.instance.lastOpenedLevel;
        if (lvlToLoad > 1 || lvlToLoad < GameManager.instance.lastOpenedLevel)
        {
            LevelText.text = lvlToLoad.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleFadeScr();
        }
    }

    public void Play()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene(lvlToLoad);
    }

    public void PrevLvl()
    {
        lvlToLoad--;
    }

    public void NextLvl()
    {
        lvlToLoad++;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ToggleFadeScr()
    {
        exitScr.SetActive(!exitScr.activeSelf);
    }

    public void HoldNextLvl()
    {
        InvokeRepeating("NextLvl", 0f, RepeatRate);
    }

    public void UnHoldLvl()
    {
        CancelInvoke();
    }

    public void HoldPrevLvl()
    {
        InvokeRepeating("PrevLvl", 0f, RepeatRate);
    }

    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/Fataler_");
    }
}