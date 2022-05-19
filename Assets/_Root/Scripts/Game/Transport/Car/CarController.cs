using Profile;
using Tool;
using UnityEngine;

namespace Game.Transport.Car
{
    internal class CarController : TransportController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Transport/Car");
        private readonly CarView _view;
        private readonly ProfilePlayer _profilePlayer;

        public override GameObject ViewGameObject => _view.gameObject;

        public override TransportModel transportModel => _profilePlayer.CurrentTransport;

        public CarController(ProfilePlayer profilePlayer)
        {
            _view = LoadView();
            _profilePlayer = profilePlayer;
        }

        private CarView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<CarView>();
        }
    }
}
