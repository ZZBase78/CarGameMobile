using Profile;
using UnityEngine;
using Services.Analytics;
using Services.Ads.UnityAds;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analytics;

    private MainController _mainController;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);

        if (_adsService.IsInitialized) OnAdsInitialized();
        else _adsService.Initialized.AddListener(OnAdsInitialized);

        _analytics.SendMainMenuOpened();
    }

    private void OnDestroy()
    {
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
        _mainController.Dispose();
    }


    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
}
