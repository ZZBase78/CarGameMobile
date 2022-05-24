using Profile;
using System;
using UnityEngine;
using Features.Inventory;
using JetBrains.Annotations;

namespace Features.Shed
{
    internal interface IShedController
    {
    }

    internal class ShedController : BaseController, IShedController
    {
        private readonly ShedView _view;
        private readonly ProfilePlayer _profilePlayer;

        private TransportUpgrader _transportUpgrader;


        public ShedController(
            [NotNull] Transform placeForUi,
            [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            var _inventoryController = new InventoryController(placeForUi, _profilePlayer.Inventory);
            AddController(_inventoryController);

            _view = new ShedFactory().CreateView(placeForUi);
            AddGameObject(_view.gameObject);

            _transportUpgrader = new TransportUpgrader(profilePlayer.CurrentTransport, profilePlayer.Inventory.EquippedItems);
            AddDisposableObject(_transportUpgrader);

            _view.Init(Apply, Back);
        }


        private void Apply()
        {
            _transportUpgrader.Upgrade();

            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Apply. Current Speed: {_profilePlayer.CurrentTransport.Speed}");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            Log($"Back. Current Speed: {_profilePlayer.CurrentTransport.Speed}");
        }
    }
}
