using Profile;
using UnityEngine;
using Services.IAP;
using Services.Analytics;
using Services.Ads.UnityAds;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private IAPService _iapService;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analytics;

    private MainController _mainController;


    private void Awake()
    {
        InitSingleServices();

        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);

        if (SingleServices.instance.adsService.IsInitialized) OnAdsInitialized();
        else SingleServices.instance.adsService.Initialized.AddListener(OnAdsInitialized);

        if (SingleServices.instance.iapService.IsInitialized) OnIapInitialized();
        else SingleServices.instance.iapService.Initialized.AddListener(OnIapInitialized);

        SingleServices.instance.analytics.SendMainMenuOpened();
    }

    private void InitSingleServices()
    {
        SingleServices.instance.adsService = _adsService;
        SingleServices.instance.iapService = _iapService;
        SingleServices.instance.analytics = _analytics;
    }

    private void OnDestroy()
    {
        SingleServices.instance.adsService.Initialized.RemoveListener(OnAdsInitialized);
        SingleServices.instance.iapService.Initialized.RemoveListener(OnIapInitialized);
        _mainController.Dispose();
    }


    private void OnAdsInitialized() => SingleServices.instance.adsService.InterstitialPlayer.Play();
    private void OnIapInitialized() => SingleServices.instance.iapService.Buy("product_1");
}
