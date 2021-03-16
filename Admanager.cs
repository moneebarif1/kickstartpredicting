
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_ANDROID
using GoogleMobileAds.Api;
#endif

public class Admanager : MonoBehaviour
{
    public static Admanager instance;

    #region Private Variables
    private BannerView bannerView;
    private InterstitialAd interstitial;
    public RewardedAd rewardedAd;
    #endregion

    #region Ad Id's
    // test id's ----------------------------------------
    private readonly string androidAppId = "ca-app-pub-3940256099942544~3347511713";
    private readonly string androidRewardedAdId = "ca-app-pub-3940256099942544/5224354917";
    private readonly string androidInterstitialAdId = "ca-app-pub-3940256099942544/1033173712";
    private readonly string androidBannerAdId = "ca-app-pub-3940256099942544/6300978111";


    //private readonly string iosAppId = "ca-app-pub-3940256099942544~1458002511";
    //private readonly string iosBannerAdId = "ca-app-pub-3940256099942544/2934735716";
    //private readonly string iosInterstitialAdID = "ca-app-pub-3940256099942544/4411468910";
    //private readonly string iosRewardedAdId = "ca-app-pub-3940256099942544/1712485313";

    //orginal id's -------------------------------------
    //private string androidAppId = "ca-app-pub-7943224880318869~6234356465";
    //private string androidBannerAdId = "";
    //private string androidInterstitialAdId = "ca-app-pub-7943224880318869/1509934390";
    //private string androidRewardedAdId = "ca-app-pub-7943224880318869/2691154129";


    //private string iosAppId = "";
    //private string iosBannerAdId = "";
    //private string iosInterstitialAdID = "";
    //private string iosRewardedAdId = "";

    #endregion
    //public GameObject RPanel;
    //public GameObject RifileText;
    public static int count_Weap1 = 0;
    public static int count_Weap2 = 0;
    public static int count_Weap3 = 0;
    public static int count_Weap4 = 0;
    public VehicleSelect AdVehSelect;
   

    int userTotalCredits = 0;

    public static String REWARDED_INSTANCE_ID = "0";


    //VehicleSelect AdVehSelect= new VehicleSelect();


    public Text rewardtext;

    //public GameObject ammoRewardText;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    public void Update()
    {
        if (PlayerPrefs.GetInt("Dummy") == 1)
        {

            Destroy(this.gameObject);
            //Debug.Log("Im Dummy");
            PlayerPrefs.SetInt("Dummy", 0);
        }
}
    #region Unity Methods
    //private void Awake()
    //{

    //    if (instance == null)
    //    {
    //        instance = this;
    //        //DontDestroyOnLoad(gameObject);

    //    }
    //    else
    //    {
    //        Destroy(this);

    //    }

    //}
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
    private void Start()
    {
        RequestAllAds();
        Debug.Log("unity-script: ShowRewardedVideoScript Start called");
        if (SceneManager.GetActiveScene().buildIndex <= 0)
        {
            AdVehSelect = GameObject.FindObjectOfType<VehicleSelect>();
            AdVehSelect.SplNextButton.SetActive(false);


            if (VehicleSelect.isSplGunUnlocked == true)
            {
                AdVehSelect.SplLock.SetActive(false);

                AdVehSelect.SplBuyBtn.SetActive(false);
                AdVehSelect.SplNextButton.SetActive(true);
            }

            initializeValues();
            //PlayerPrefs.DeleteKey("Weap1");
            //PlayerPrefs.DeleteKey("Weap2");
            //PlayerPrefs.DeleteKey("Weap3");
            //PlayerPrefs.DeleteKey("Weap4");
            Ad_Check(); // PlayerPrefs check



            Debug.Log(count_Weap1);
            Debug.Log(count_Weap2);
            Debug.Log(count_Weap3);
            Debug.Log(count_Weap4);

            #region IronSource RewardedCallbacks declaration
            //Add Rewarded Video Events
            //IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
            //IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
            //IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
            //IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
            //IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
            //IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
            //IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
            //IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;

            ////Add Rewarded Video DemandOnly Events
            //IronSourceEvents.onRewardedVideoAdOpenedDemandOnlyEvent += RewardedVideoAdOpenedDemandOnlyEvent;
            //IronSourceEvents.onRewardedVideoAdClosedDemandOnlyEvent += RewardedVideoAdClosedDemandOnlyEvent;
            //IronSourceEvents.onRewardedVideoAdLoadedDemandOnlyEvent += this.RewardedVideoAdLoadedDemandOnlyEvent;
            //IronSourceEvents.onRewardedVideoAdRewardedDemandOnlyEvent += RewardedVideoAdRewardedDemandOnlyEvent;
            //IronSourceEvents.onRewardedVideoAdShowFailedDemandOnlyEvent += RewardedVideoAdShowFailedDemandOnlyEvent;
            //IronSourceEvents.onRewardedVideoAdClickedDemandOnlyEvent += RewardedVideoAdClickedDemandOnlyEvent;
            //IronSourceEvents.onRewardedVideoAdLoadFailedDemandOnlyEvent += this.RewardedVideoAdLoadFailedDemandOnlyEvent;
            #endregion


        }
    }
    #endregion
    public void DisableNext()
    {
        AdVehSelect.SplNextButton.SetActive(false);
    }

    public void SplBuyAD_Btn()
    {
        AdVehSelect.SplBuyBtn.SetActive(true);
    }
     public void ShowRewardedAd()
     {

        //Debug.Log("unity-script: ShowRewardedVideoButtonClicked");


        //if (IronSource.Agent.isRewardedVideoAvailable())
        if (rewardedAd != null && rewardedAd.IsLoaded())
        {
            //IronSource.Agent.showRewardedVideo();

            ShowRewardedGoogleAds();
            WeaponADS();
            SplBuyCheck();
        }

        else
        {
            //Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
        }

     }

    public void SplBuyCheck()
    {
        if (VehicleSelect.isSplGunUnlocked == true)
        {
            AdVehSelect.SplLock.SetActive(false);
            AdVehSelect.SplBuyBtn.SetActive(false);
            AdVehSelect.SplNextButton.SetActive(true);
        }

        if (VehicleSelect.isSplGunUnlocked == false)
        {
            if (AdVehSelect.ID == 14)
            {
                if (count_Weap1 == 4)
                {
                    AdVehSelect.SplLock.SetActive(false);
                    AdVehSelect.SplBuyBtn.SetActive(false);
                }
            }


            if (AdVehSelect.ID == 15)
            {
                if (count_Weap2 == 6)
                {
                    AdVehSelect.SplLock.SetActive(false);
                    AdVehSelect.SplBuyBtn.SetActive(false);
                }
            }


            if (AdVehSelect.ID == 16)
            {
                if (count_Weap3 == 8)
                {
                    AdVehSelect.SplLock.SetActive(false);
                    AdVehSelect.SplBuyBtn.SetActive(false);
                }
            }


            if (AdVehSelect.ID == 17)
            {
                if (count_Weap4 == 10)
                {
                    AdVehSelect.SplLock.SetActive(false);
                    AdVehSelect.SplBuyBtn.SetActive(false);
                }
            }
        }
    }

    public void NextBtn_SpecialAD()
    {
        if (VehicleSelect.isSplGunUnlocked == true)
        {
            AdVehSelect.SplLock.SetActive(false);
            AdVehSelect.SplBuyBtn.SetActive(false);
            AdVehSelect.SplNextButton.SetActive(true);
        }

        if (VehicleSelect.isSplGunUnlocked == false)
        {
            if (AdVehSelect.ID == 14)
            {
                rewardtext.text = "(" + count_Weap1 + " / 4)";
                if (count_Weap1 == 4)
                {
                    AdVehSelect.SplLock.SetActive(false);
                    rewardtext.text = "Unlocked";
                }
            }

            if (AdVehSelect.ID == 15)
            {
                rewardtext.text = "(" + count_Weap2 + " / 6)";
                if (count_Weap2 == 6)
                {
                    AdVehSelect.SplLock.SetActive(false);
                    rewardtext.text = "Unlocked";
                }
            }

            if (AdVehSelect.ID == 16)
            {
                rewardtext.text = "(" + count_Weap3 + " / 8)";
                if (count_Weap3 == 8)
                {
                    AdVehSelect.SplLock.SetActive(false);
                    rewardtext.text = "Unlocked";
                }
            }

            if (AdVehSelect.ID == 17)
            {
                rewardtext.text = "(" + count_Weap4 + " / 10)";
                if (count_Weap4 == 10)
                {
                    AdVehSelect.SplLock.SetActive(false);
                    rewardtext.text = "Unlocked";
                }
            }

            AdVehSelect.SplLock.SetActive(true);
        }
    }

    public void Ad_Check()
    {
        //HasKey ---> this return true if it exists or else it returns false
        if (VehicleSelect.isSplGunUnlocked == true)
        {
            AdVehSelect.SplLock.SetActive(false);
            AdVehSelect.SplBuyBtn.SetActive(false);
            AdVehSelect.SplNextButton.SetActive(true);
        }

    }

    public void initializeValues()
    {
        // Special Gun 1
        if (PlayerPrefs.HasKey("Weap1") == false)
        {
            PlayerPrefs.SetInt("Weap1", 0);
        }
        Debug.Log(count_Weap1);

        //Special Gun 2
        if (PlayerPrefs.HasKey("Weap2") == false)
        {
            PlayerPrefs.SetInt("Weap2", 0);
        }

        //Special Gun 3
        if (PlayerPrefs.HasKey("Weap3") == false)
        {
            PlayerPrefs.SetInt("Weap3", 0);
        }

        // Special Gun 4
        if (PlayerPrefs.HasKey("Weap4") == false)
        {
            PlayerPrefs.SetInt("Weap4", 0);
        }

        PlayerPrefs.Save();
        count_Weap1 = PlayerPrefs.GetInt("Weap1");
        count_Weap2 = PlayerPrefs.GetInt("Weap2");
        count_Weap3 = PlayerPrefs.GetInt("Weap3");
        count_Weap4 = PlayerPrefs.GetInt("Weap4");
    }
    public void WeaponADS()
    {
        if (VehicleSelect.isSplGunUnlocked == true)
        {
            AdVehSelect.SplLock.SetActive(false);
            AdVehSelect.SplBuyBtn.SetActive(false);
            AdVehSelect.SplNextButton.SetActive(true);
        }

        if (VehicleSelect.isSplGunUnlocked == false)
        {
            if (AdVehSelect.ID == 14)
            {
                if (count_Weap1 == 4)
                {
                    rewardtext.text = "(" + count_Weap1 + " / 4)";
                    Debug.Log("You Have unlocked" + AdVehSelect.ID);
                    AdVehSelect.SplNextButton.SetActive(true);
                }
                else
                {
                    count_Weap1++;
                    rewardtext.text = "(" + count_Weap1 + " / 4)";
                    PlayerPrefs.SetInt("Weap1", count_Weap1);
                }

                if (count_Weap1 == 4)
                {
                    AdVehSelect.SplNextButton.SetActive(true);
                }
            }

            if (AdVehSelect.ID == 15)
            {
                count_Weap2++;
                rewardtext.text = "(" + count_Weap2 + " / 6)";
                PlayerPrefs.SetInt("Weap2", count_Weap2);
                if (count_Weap2 == 6)
                {
                    rewardtext.text = "(" + count_Weap2 + " / 6)";
                    Debug.Log("You Have unlocked " + AdVehSelect.ID);
                    AdVehSelect.SplNextButton.SetActive(true);
                }
            }

            if (AdVehSelect.ID == 16)
            {
                count_Weap3++;
                rewardtext.text = "(" + count_Weap3 + " / 8)";
                PlayerPrefs.SetInt("Weap3", count_Weap3);

                if (count_Weap3 == 8)
                {
                    rewardtext.text = "(" + count_Weap3 + " / 8)";
                    Debug.Log("You Have unlocked " + AdVehSelect.ID);
                    AdVehSelect.SplNextButton.SetActive(true);
                }
            }

            if (AdVehSelect.ID == 17)
            {
                count_Weap4++;
                rewardtext.text = "(" + count_Weap4 + " / 10)";
                PlayerPrefs.SetInt("Weap4", count_Weap4);

                if (count_Weap4 == 10)
                {
                    rewardtext.text = "(" + count_Weap4 + " / 10)";
                    Debug.Log("You Have unlocked " + AdVehSelect.ID);
                    AdVehSelect.SplNextButton.SetActive(true);
                }
            }
            PlayerPrefs.Save();
        }
    }

    /*public void CountReset()
	{
		count = 0;
	}*/


    void OnDisable()
    {
        PlayerPrefs.SetInt("Weap1", count_Weap1);

        PlayerPrefs.SetInt("Weap2", count_Weap2);

        PlayerPrefs.SetInt("Weap3", count_Weap3);

        PlayerPrefs.SetInt("Weap4", count_Weap4);
        PlayerPrefs.Save();
    }

    public void Gun14_Check()
    {
        if (AdVehSelect.ID == 14)
        {
            rewardtext.text = "(" + count_Weap1 + " / 4)";
            if (count_Weap1 == 4)
            {
                AdVehSelect.SplLock.SetActive(false);
            }

            else
            {
                AdVehSelect.SplLock.SetActive(true);
            }
        }

        //AdVehSelect.SplLock.SetActive(true);
    }
    public void RequestAllAds()
    {
        //RequestBanner();
        RequestInterstitial();
        //IronSource.Agent.loadInterstitial();
        CreateAndLoadRewardedAd();
    }

    //public IEnumerator ammoText()
    //{
    //    ammoRewardText.SetActive(true);
    //    yield return new WaitForSeconds(6f);
    //    ammoRewardText.SetActive(false);
    //}

    public void SplGunChecking()
    {
        if (VehicleSelect.isSplGunUnlocked == true)
        {
            //IronSource.Agent.showRewardedVideo();

            WeaponADS();

            SplBuyCheck();
        }
    }
    #region IronSource Callbacks
    /************* RewardedVideo Delegates *************/
    //  void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
    //  {
    //      Debug.Log("unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + canShowAd);
    //      if (canShowAd)
    //      {
    //      }
    //      else
    //      {
    //      }
    //  }

    //  void RewardedVideoAdOpenedEvent()
    //  {
    //      Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent");
    //  }

    //  void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    //  {
    //      Debug.Log("unity-script: I got RewardedVideoAdRewardedEvent, amount = " + ssp.getRewardAmount() + " name = " + ssp.getRewardName());
    //      //userTotalCredits = userTotalCredits + ssp.getRewardAmount ();

    //      Time.timeScale = 1;

    //  }

    //  void RewardedVideoAdClosedEvent()
    //  {
    //      Debug.Log("unity-script: I got RewardedVideoAdClosedEvent");
    //      //StartCoroutine(ammoText());

    //      /*if (SceneManager.sceneCount == 1)
    //{
    //	StartCoroutine(ammoText());
    //}*/

    //  }

    //void RewardedVideoAdStartedEvent()
    //{
    //    Debug.Log("unity-script: I got RewardedVideoAdStartedEvent");
    //}

    //void RewardedVideoAdEndedEvent()
    //{
    //    Debug.Log("unity-script: I got RewardedVideoAdEndedEvent");
    //}

    //void RewardedVideoAdShowFailedEvent(IronSourceError error)
    //{
    //    Debug.Log("unity-script: I got RewardedVideoAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    //}

    //void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
    //{
    //    Debug.Log("unity-script: I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName());
    //}

    ///************* RewardedVideo DemandOnly Delegates *************/

    //void RewardedVideoAdLoadedDemandOnlyEvent(string instanceId)
    //{
    //    Debug.Log("unity-script: I got RewardedVideoAdLoadedDemandOnlyEvent for instance: " + instanceId);
    //}

    //void RewardedVideoAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    //{
    //    Debug.Log("unity-script: I got RewardedVideoAdLoadFailedDemandOnlyEvent for instance: " + instanceId + ", code :  " + error.getCode() + ", description : " + error.getDescription());
    //}

    //void RewardedVideoAdOpenedDemandOnlyEvent(string instanceId)
    //{
    //    Debug.Log("unity-script: I got RewardedVideoAdOpenedDemandOnlyEvent for instance: " + instanceId);
    //}

    //void RewardedVideoAdRewardedDemandOnlyEvent(string instanceId)
    //{
    //    Debug.Log("unity-script: I got RewardedVideoAdRewardedDemandOnlyEvent for instance: " + instanceId);
    //}

    //void RewardedVideoAdClosedDemandOnlyEvent(string instanceId)
    //{
    //    Debug.Log("unity-script: I got RewardedVideoAdClosedDemandOnlyEvent for instance: " + instanceId);
    //}

    //void RewardedVideoAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    //{
    //    Debug.Log("unity-script: I got RewardedVideoAdShowFailedDemandOnlyEvent for instance: " + instanceId + ", code :  " + error.getCode() + ", description : " + error.getDescription());
    //}

    //void RewardedVideoAdClickedDemandOnlyEvent(string instanceId)
    //{
    //    Debug.Log("unity-script: I got RewardedVideoAdClickedDemandOnlyEvent for instance: " + instanceId);
    //}
    #endregion
    #region Rewarded Ads GoogleAdMobs
    public void CreateAndLoadRewardedAd()
    {
        //if (!UnityRemoteData.isFullAdsEnabled)
        //{
        //    return;
        //}

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = androidRewardedAdId;
#elif UNITY_IPHONE
        string adUnitId = iosRewardedAdId;
#else
        string adUnitId = "unexpected_platform";
#endif
        if (rewardedAd == null || !rewardedAd.IsLoaded())
        {
            // Create new rewarded ad instance.
            rewardedAd = new RewardedAd(adUnitId);

            // Called when an ad request has successfully loaded.
            rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            // Called when an ad request failed to load.
            rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            // Called when an ad is shown.
            rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            // Called when an ad request failed to show.
            rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            // Called when the user should be rewarded for interacting with the ad.
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            rewardedAd.OnAdClosed += HandleRewardedAdClosed;

            // Create an empty ad request.
            AdRequest request = CreateAdRequest();
            // Load the rewarded ad with the request.
            rewardedAd.LoadAd(request);

        }

    }

    public void ShowRewardedGoogleAds()//GoogleAD
    {
        //currentType = type;

        //Debug.Log("isFull ads Enabled-------->" + UnityRemoteData.isFullAdsEnabled);
        //if (!UnityRemoteData.isFullAdsEnabled)
        //{
        //    return;
        //}

        if (rewardedAd != null && rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
            CreateAndLoadRewardedAd();
        }
    }
    #region RewardedAd callback handlers

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        CreateAndLoadRewardedAd();
        MonoBehaviour.print("HandleRewardedAdOpening event received");

    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: " + args.Message);

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");


    }

    public int rewardAmount;

    public void HandleUserEarnedReward(object sender, Reward args)
    {

        string type = args.Type;
        double amount = args.Amount;

        MonoBehaviour.print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);


        CreateAndLoadRewardedAd();

    }
    #endregion

    #endregion
    #region Banner GoogleAdMobs
    public void RequestBanner()
    {
        //showAds = PlayerPrefs.GetInt(PlayerPrefsManager.SHOW_ADS, 0);
        //if (showAds == 1)
        //{
        //    return;
        //}

        //if (!UnityRemoteData.isFullAdsEnabled || !UnityRemoteData.isBannerAdsEnabled)
        //{
        //    return;
        //}

        // Clean up banner ad before creating a new one.
        if (bannerView != null)
        {
            return;
        }


        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = androidBannerAdId;
#elif UNITY_IPHONE
        string adUnitId = iosBannerAdId;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        AdSize adsize = AdSize.SmartBanner/*GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth)*/;
        bannerView = new BannerView(adUnitId, adsize, AdPosition.Top);

        // Register for ad events.
        bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        bannerView.OnAdOpening += HandleAdOpened;
        bannerView.OnAdClosed += HandleAdClosed;
        bannerView.OnAdLeavingApplication += HandleAdLeftApplication;

        // Load a banner ad.
        bannerView.LoadAd(CreateAdRequest());
        //ShowBanner();
    }
    public void DestroyBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    public void HideBanner()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
        }
    }
    public void ShowBanner()
    {

        //showAds = PlayerPrefs.GetInt(PlayerPrefsManager.SHOW_ADS, 0);

        //if (showAds == 1)
        //{
        //    return;
        //}

        //if (!UnityRemoteData.isFullAdsEnabled || !UnityRemoteData.isBannerAdsEnabled)
        //{
        //    return;
        //}


        if (bannerView != null)
        {
            Debug.Log("Banner Ad Showed-------->");
            bannerView.Show();
        }
        else
        {
            //RequestBanner();
        }
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {

        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    #endregion
    #endregion



    #region InterstitialGoogleAds
    public void RequestInterstitial()
    {
        //showAds = PlayerPrefs.GetInt(PlayerPrefsManager.SHOW_ADS, 0);
        //if (showAds == 1)
        //{
        //    return;
        //}

        //if (PlayerPrefs.GetInt(PlayerPrefsManager.CompletedLevels) < UnityRemoteData.adsStartlevel)
        //{
        //    return;
        //}

        //if (!UnityRemoteData.isFullAdsEnabled || !UnityRemoteData.isInterstitialAdsEnabled)
        //{
        //    return;
        //}

        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = androidInterstitialAdId;
#elif UNITY_IPHONE
        string adUnitId = iosInterstitialAdID;
#else
        string adUnitId = "unexpected_platform";
#endif
        if (interstitial == null || !interstitial.IsLoaded())
        {

            // Create an interstitial.
            interstitial = new InterstitialAd(adUnitId);

            // Register for ad events.
            interstitial.OnAdLoaded += HandleInterstitialLoaded;
            interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
            interstitial.OnAdOpening += HandleInterstitialOpened;
            interstitial.OnAdClosed += HandleInterstitialClosed;

            // Load an interstitial ad.
            interstitial.LoadAd(CreateAdRequest());
        }
    }


    public void ShowInterstitialWin()
    {
        if (interstitial != null && interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            RequestInterstitial();
        }
    }
    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        
        MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialClosed event received");
	interstitial.Destroy();
	interstitial=null;
	RequestInterstitial();
    }

    #endregion
    #endregion

   
    public void RequestingAds()
    {
        //ShowBanner();
        //RequestBanner();
        //RequestInterstitial();
        CreateAndLoadRewardedAd();
    }
   
}