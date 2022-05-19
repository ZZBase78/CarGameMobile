using Tool;
using System;
using Profile;
using Game.InputLogic;
using Game.TapeBackground;
using Game.Transport;
using Game.Transport.Boat;
using Game.Transport.Car;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly ProfilePlayer _profilePlayer;
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly TapeBackgroundController _tapeBackgroundController;
        private readonly InputGameController _inputGameController;
        private readonly TransportController _transportController;


        public GameController(ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _tapeBackgroundController = CreateTapeBackground();
            _inputGameController = CreateInputGameController();
            _transportController = CreateTransportController();
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

        private TransportController CreateTransportController()
        {
            TransportController transportController =
                _profilePlayer.CurrentTransport.Type switch
                {
                    TransportType.Car => new CarController(),
                    TransportType.Boat => new BoatController(),
                    _ => throw new ArgumentException(nameof(TransportType))
                };

            AddController(transportController);

            return transportController;
        }
    }
}
