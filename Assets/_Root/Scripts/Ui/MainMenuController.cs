using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Purchasing;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, Rewarded, BuyItem);
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

        private void Rewarded()
        {
            SingleServices.instance.adsService.RewardedPlayer.Play();
        }

        private void BuyItem()
        {
            SingleServices.instance.iapService.actionBuyItemComplete += BuyItemSuccess;
            SingleServices.instance.iapService.Buy("product_3");
        }

        private void BuyItemSuccess(PurchaseEventArgs args)
        {
            SingleServices.instance.iapService.actionBuyItemComplete -= BuyItemSuccess;

            SingleServices.instance.analytics.Transaction(args.purchasedProduct.definition.id, args.purchasedProduct.metadata.localizedPrice, args.purchasedProduct.metadata.isoCurrencyCode);

            Debug.Log("Buy complete: " + args.purchasedProduct.definition.id);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            SingleServices.instance.iapService.actionBuyItemComplete -= BuyItemSuccess;
        }
    }
}
