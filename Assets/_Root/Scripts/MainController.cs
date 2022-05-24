using Ui;
using Game;
using Profile;
using UnityEngine;
using System.Collections.Generic;
using Features.Shed.Upgrade;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private GameController _gameController;
    private ShedControllerFactory _shedControllerFactory;
    private TransportUpgrader _transportUpgrader;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);

        _shedControllerFactory = new ShedControllerFactory(profilePlayer, placeForUi);
        _transportUpgrader = new TransportUpgrader(profilePlayer.CurrentTransport, profilePlayer.Inventory.EquippedItems);
    }

    protected override void OnDispose()
    {
        DisposeAllControllers();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        DisposeAllControllers();

        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _shedControllerFactory.Display(OnShedDone, OnShedBack);
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer);
                break;
        }
    }

    private void DisposeAllControllers()
    {
        _mainMenuController?.Dispose();
        _settingsMenuController?.Dispose();
        _gameController?.Dispose();
    }

    private void OnShedBack()
    {
        _shedControllerFactory.Destroy();
        _profilePlayer.CurrentState.Value = GameState.Start;
    }

    private void OnShedDone(IReadOnlyDictionary<string, IUpgradeHandler> upgradeItems)
    {
        _transportUpgrader.Upgrade(upgradeItems);
        _shedControllerFactory.Destroy();
        _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
