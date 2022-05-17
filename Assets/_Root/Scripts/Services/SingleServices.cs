using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class SingleServices
{
    public IAPService iapService;
    public UnityAdsService adsService;
    public AnalyticsManager analytics;

    private static SingleServices _instance;
    public static SingleServices instance
    {
        get
        {
            if (_instance == null) _instance = new SingleServices();
            return _instance;
        }
    }
    private SingleServices()
    {

    }
}
