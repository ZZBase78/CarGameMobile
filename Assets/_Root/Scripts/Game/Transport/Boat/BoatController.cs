using Profile;
using Tool;
using UnityEngine;

namespace Game.Transport.Boat
{
    internal class BoatController : TransportController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Transport/Boat");
        private readonly BoatView _view;
        private readonly ProfilePlayer _profilePlayer;

        public override GameObject ViewGameObject => _view.gameObject;

        public override TransportModel transportModel => _profilePlayer.CurrentTransport;

        public BoatController(ProfilePlayer profilePlayer)
        {
            _view = LoadView();
            _profilePlayer = profilePlayer;
        }

        private BoatView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<BoatView>();
        }
    }
}
