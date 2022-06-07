using Tool;
using System;
using Profile;
using Services;
using UnityEngine;
using Game.InputLogic;
using Game.TapeBackground;
using Game.Transport;
using Game.Transport.Boat;
using Game.Transport.Car;
using Features.AbilitySystem;
using Features.Fight;
using Ui;

namespace Game
{
    internal class GameController : BaseController
    {
        private Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly TapeBackgroundController _tapeBackgroundController;
        private readonly TransportController _transportController;
        private readonly AbilitiesMvcContainer _abilitiesContainer;
        private InputGameController _inputGameController;
        private StartFightController _startFightController;
        private CloseButtonController _closeButtonController;
        private PauseButtonController _pauseButtonController;
        private FightController _fightController;
        private PauseMenuController _pauseMenuController;
        private SettingsMenuController _settingsMenuController;

        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _tapeBackgroundController = CreateTapeBackground();
            _inputGameController = CreateInputGameController();
            _transportController = CreateTransportController(profilePlayer.CurrentTransport);
            _abilitiesContainer = CreateAbilitiesContainer(_transportController, placeForUi);

            _startFightController = new StartFightController(placeForUi, _profilePlayer);
            _startFightController.OnFightButtonPressed += StartFightButtonPressed;
            AddController(_startFightController);
            _closeButtonController = new CloseButtonController(placeForUi, _profilePlayer);
            AddController(_closeButtonController);
            _pauseButtonController = new PauseButtonController(placeForUi, _profilePlayer);
            _pauseButtonController.OnPauseButtonPressed += PauseButtonPressed;
            AddController(_pauseButtonController);


            ServiceRoster.Analytics.SendGameStarted();
        }

        private void StartFightButtonPressed()
        {
            _inputGameController.SetActive(false);
            _fightController = new FightController(_placeForUi, _profilePlayer);
            _fightController.OnEndFight += OnEndFight;
        }

        private void OnEndFight()
        {
            _fightController?.Dispose();
            _inputGameController.SetActive(true);
        }

        private void PauseButtonPressed()
        {
            _inputGameController.SetActive(false);
            _pauseMenuController = new PauseMenuController(_placeForUi, _profilePlayer);
            _pauseMenuController.OnButtonExitPressed += PauseOnExitPressed;
            _pauseMenuController.OnButtonContinuePressed += PauseOnContinuePressed;
            _pauseMenuController.OnButtonSettingsPressed += PauseOnSettingsPressed;
        }

        private void PauseOnExitPressed()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
        }
        private void PauseOnContinuePressed()
        {
            _pauseMenuController?.Dispose();
            _inputGameController.SetActive(true);
        }

        private void PauseOnSettingsPressed()
        {
            _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
            _settingsMenuController.OnBackPressed += OnSettingsBackPressed;
        }

        private void OnSettingsBackPressed()
        {
            _settingsMenuController?.Dispose();
        }

        private TapeBackgroundController CreateTapeBackground()
        {
            var tapeBackgroundController = new TapeBackgroundController(_leftMoveDiff, _rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }

        private InputGameController CreateInputGameController()
        {
            var inputGameController = new InputGameController(_leftMoveDiff, _rightMoveDiff, _profilePlayer.CurrentTransport);
            AddController(inputGameController);

            return inputGameController;
        }

        private TransportController CreateTransportController(TransportModel transportModel)
        {
            TransportController transportController =
                _profilePlayer.CurrentTransport.Type switch
                {
                    TransportType.Car => new CarController(transportModel),
                    TransportType.Boat => new BoatController(transportModel),
                    _ => throw new ArgumentException(nameof(TransportType))
                };

            AddController(transportController);

            return transportController;
        }

        private AbilitiesMvcContainer CreateAbilitiesContainer(IAbilityActivator abilityActivator, Transform placeForUi)
        {
            var container = new AbilitiesMvcContainer(abilityActivator, placeForUi);
            AddContainer(container);

            return container;
        }

        protected override void OnDispose()
        {
            if (_startFightController != null) _startFightController.OnFightButtonPressed -= StartFightButtonPressed;
            if (_pauseButtonController != null) _pauseButtonController.OnPauseButtonPressed -= PauseButtonPressed;
            if (_pauseMenuController != null) _pauseMenuController.OnButtonExitPressed -= PauseOnExitPressed;
            if (_settingsMenuController != null) _settingsMenuController.OnBackPressed -= OnSettingsBackPressed;

            _startFightController?.Dispose();
            _pauseButtonController?.Dispose();
            _pauseMenuController?.Dispose();
            _settingsMenuController?.Dispose();
            base.OnDispose();
        }
    }
}
