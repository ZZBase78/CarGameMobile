using Profile;
using Tool;
using UnityEngine;

internal class CloseButtonController : BaseController
{
    private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/CloseButton/CloseButton");

    private readonly CloseButtonView _view;
    private readonly ProfilePlayer _profilePlayer;


    public CloseButtonController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.CloseButton.onClick.AddListener(OnButtonPressedListener);
    }

    private void OnButtonPressedListener()
    {
        _profilePlayer.CurrentState.Value = GameState.Start;
    }

    protected override void OnDispose()
    {
        _view.CloseButton.onClick.RemoveAllListeners();
        base.OnDispose();
    }


    private CloseButtonView LoadView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
        GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<CloseButtonView>();
    }

}
