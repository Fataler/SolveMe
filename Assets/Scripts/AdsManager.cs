using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System;

/// <summary>
/// ADSManager singleton with Unty/Google Ads.
/// </summary>
public class AdsManager : Singleton<AdsManager>, IUnityAdsListener
{
    public enum AdsService
    {
        AdMob,
        Unity
    }

    //ads
#if UNITY_ANDROID || UNITY_EDITOR
    private const string AdId = SensativeData.ANDROIDADMOBID;
#elif UNITY_IOS
    private const string AdId =  SensativeData.IOSADMOBID;
#endif
    public AdsService service;

    //unity
    private const string VideoType = "rewardedVideo";

    private const string Video = "video";
    public bool testMode = true;

    //admob
    private const string Rewarded = SensativeData.UNITYREWARDEDVIDEO;

    private RewardBasedVideoAd rewardBasedVideo;
    private const string Interstitial = SensativeData.UNITYINTERSTITIALVIDEO;

    public bool adIsRunning = false;

    private void Start()
    {
        if (service == AdsService.Unity)
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(AdId, testMode);
        }
        else
        {
            MobileAds.Initialize(initStatus => { });
            this.rewardBasedVideo = RewardBasedVideoAd.Instance;
            this.RequestRewardBasedVideo();
        }
    }

    private void OnDisable()
    {
        rewardBasedVideo.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdOpening -= HandleRewardBasedVideoOpened;
        rewardBasedVideo.OnAdClosed -= HandleRewardBasedVideoClosed;
    }

    private void RequestRewardBasedVideo()
    {
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, Rewarded);

        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
    }

    private void HandleRewardBasedVideoOpened(object sender, EventArgs e)
    {
        adIsRunning = true;
    }

    private void HandleRewardBasedVideoClosed(object sender, EventArgs e)
    {
        adIsRunning = false;
        var rootItems = SceneManager.GetActiveScene().GetRootGameObjects();
        Debug.LogWarning("Admob loaded.");
        foreach (var item in rootItems)
        {
            item.BroadcastMessage("AdsDidFinish", SendMessageOptions.DontRequireReceiver);
        }
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardBasedVideo.LoadAd(request, Rewarded);
    }

    private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        var rootItems = SceneManager.GetActiveScene().GetRootGameObjects();
        Debug.LogWarning("The ad did not finish due to an error.");
        foreach (var item in rootItems)
        {
            item.BroadcastMessage("AdsDidFinish", SendMessageOptions.DontRequireReceiver);
        }
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardBasedVideo.LoadAd(request, Rewarded);
    }

    public void ShowAd()
    {
        if (service == AdsService.Unity)
        {
            //admob rewarded ads
            Advertisement.Show(VideoType);
        }
        else
        {
            //admob rewarded ads
            if (rewardBasedVideo.IsLoaded())
            {
                rewardBasedVideo.Show();
            }
        }
    }

    public void ShowAd(string type)
    {
        if (service == AdsService.Unity)
        {
            //unity Interstitial ads

            Advertisement.Show(type);
        }
        else
        {
            //TODO emplement admob Interstitial ads
        }
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        adIsRunning = false;
        if (placementId == Video)
        {
            return;
        }
        var rootItems = SceneManager.GetActiveScene().GetRootGameObjects();
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            foreach (var item in rootItems)
            {
                item.BroadcastMessage("AdsDidFinish", SendMessageOptions.DontRequireReceiver);
            }
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            foreach (var item in rootItems)
            {
                item.BroadcastMessage("AdsDidFinish", SendMessageOptions.DontRequireReceiver);
            }
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
            foreach (var item in rootItems)
            {
                item.BroadcastMessage("AdsDidError", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        adIsRunning = true;
    }

    public void OnUnityAdsReady(string placementId)
    {
        //stab :(
    }
}