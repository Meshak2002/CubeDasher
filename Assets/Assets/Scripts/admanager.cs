using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class admanager : MonoBehaviour, IUnityAdsListener
{
    private const string game_Id = "";
    bool test_mode = true; // override this in unity ads panel
    [Header("AD-ID")]
    [SerializeField] string interstitalAdId;
    [SerializeField] string rewardedAdId;
    [SerializeField] string bannerAdId;

    int i;
    [Space(10f)]
    public PlayerCollision player;
    private bool isClicked = false;

    [SerializeField] private string sceneName;
    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(game_Id, test_mode);
        //Advertisement.Banner.SetPosition(BannerPosition._DEFINE_POSITION_);by default bottom center
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(ShowBannerWhenReady());
        }
    }
    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(bannerAdId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(bannerAdId);
    }
    public void ShowInteristialAd()
    {
        if (Advertisement.IsReady(interstitalAdId))
        {
            Advertisement.Show(interstitalAdId);
            i = 0;
        }
        else
        {
            SceneManager.LoadScene("CutScene");
        }
    }
    public void ShowRewardedVideo()
    {
           
        
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(rewardedAdId))
        {
            Advertisement.Show(rewardedAdId);
            i = 1;
            isClicked = true;
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
            isClicked = true;

        }
    }

   


    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            if (i == 1)
            {
                player.rewardPlayer();
            }
            else if (i == 0)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            // ad skipped dont reward
            player.dontrewardPlayer();
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
            player.dontrewardPlayer();
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == "Rewarded_Android")
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
