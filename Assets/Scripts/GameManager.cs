using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

/// <summary>
/// Main Game manager.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    //settings
    [Header("Ads settings")]
    public int adsEvery = 7;

    [Header("Current Level Info")]
    public int currentLevel = 0;

    public int lastOpenedLevel;

    [Header("FPS config")]
    public int targetFrameRate = 30;

    [SerializeField]
    private int _totallevels;

    //animations

    private GameObject _currentAnimation;

    private GameObject _fadeIns;

    private bool _isChanging;

    [Space]
    //analytics
    public float timer = 0.0f;

    //Tips
    private TipsList _list = new TipsList();

    public void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        lastOpenedLevel = PlayerPrefs.GetInt("last_level", 1);
        SceneManager.activeSceneChanged += SceneChanged;

        //fps stuff
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;

        //Levels Stuff
        _totallevels = SceneManager.sceneCountInBuildSettings;

        //fadeins stuff
        FindFadeIns();

        //get tips
        InitJson();
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= SceneChanged;

        //send exit game analytics
        Analytics.CustomEvent("ExitGame", new Dictionary<string, object>
        {
            { "level_index",currentLevel }
        });
    }

    private void SceneChanged(Scene arg0, Scene arg1)
    {
        FindFadeIns();
        timer = 0f;
        var lastLevel = currentLevel;
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        // save game progress
        if (currentLevel > lastOpenedLevel)
        {
            lastOpenedLevel = currentLevel;
            PlayerPrefs.SetInt("last_level", lastOpenedLevel);
        }

        //ads every N levels
        if (lastLevel != 0)
        {
            ShowAds();
        }
    }

    private void ShowAds()
    {
        if (currentLevel % adsEvery == 0 &&
                    GameManager.instance.currentLevel != 0 &&
                    GameManager.instance.currentLevel != _totallevels)
        {
            if (AdsManager.instance != null)
            {
                AdsManager.instance.ShowAd("video");
            }
        }
    }

    #region Animations

    public void FindFadeIns()
    {
        _fadeIns = GameObject.Find("FadeIns");
        if (_fadeIns != null)
        {
            _currentAnimation = _fadeIns.transform.GetChild(Random.Range(0, _fadeIns.transform.childCount - 1)).gameObject;
            _currentAnimation.SetActive(true);
        }
    }

    #endregion Animations

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
    }

    #region LevelChanging

    public void NextLevel()
    {
        var animator = _currentAnimation.GetComponent<Animator>();

        if (!_isChanging)
        {
            Analytics.CustomEvent("level_complete", new Dictionary<string, object>
        {
                { "level_index",currentLevel },
                {"timer",timer }
        });
            StartCoroutine(ChangeLvl(animator));
        }
    }

    private IEnumerator ChangeLvl(Animator animator)
    {
        _isChanging = true;
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("Win");
        }

        yield return new WaitForSeconds(0.8f);
        if (animator != null)
        {
            animator.SetBool("next", true);
        }
        yield return new WaitForSeconds(1f);
        if (currentLevel >= _totallevels - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentLevel + 1);
        }
        _isChanging = false;
    }

    #endregion LevelChanging

    #region TipsGathering

    private void InitJson()
    {
        TextAsset file = Resources.Load("Tips") as TextAsset;
        _list = JsonUtility.FromJson<TipsList>(file.text);
    }

    public List<string> GetTips(int level)
    {
        if (level > _list.Tips.Count)
        {
            return null;
        }
        var tips = _list.Tips[level];
        var result = new List<string>
        {
            tips.tip1,
            tips.tip2,
            tips.tip3
        };
        if (result != null)
        {
            return result;
        }
        else
        {
            Debug.LogError("There is no Hint for that level");

            return null;
        }
    }

    #endregion TipsGathering
}