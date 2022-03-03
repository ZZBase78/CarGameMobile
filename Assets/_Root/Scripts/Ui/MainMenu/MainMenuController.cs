using Tool;
using Profile;
using Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, Settings, Shed, Reward);

            SubscribeAds();
        }

        protected override void OnDispose()
        {
            UnsubscribeAds();
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void Settings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void Shed() =>
            _profilePlayer.CurrentState.Value = GameState.Shed;

        private void Reward() =>
            ServiceLocator.AdsService.RewardedPlayer.Play();

        private void SubscribeAds()
        {
            ServiceLocator.AdsService.RewardedPlayer.Finished += OnAdsFinished;
            ServiceLocator.AdsService.RewardedPlayer.Failed += OnAdsCancelled;
            ServiceLocator.AdsService.RewardedPlayer.Skipped += OnAdsCancelled;
        }

        private void UnsubscribeAds()
        {
            ServiceLocator.AdsService.RewardedPlayer.Finished -= OnAdsFinished;
            ServiceLocator.AdsService.RewardedPlayer.Failed -= OnAdsCancelled;
            ServiceLocator.AdsService.RewardedPlayer.Skipped -= OnAdsCancelled;
        }

        private void OnAdsFinished() => Log("You've received a reward for ads!");
        private void OnAdsCancelled() => Log("Receiving a reward for ads has been interrupted!");
    }
}
