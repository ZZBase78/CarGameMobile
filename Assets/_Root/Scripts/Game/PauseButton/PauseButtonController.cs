using Profile;
using System;
using Tool;
using UnityEngine;

internal class PauseButtonController : BaseController
{
    public event Action OnPauseButtonPressed = delegate { };

    private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/PauseButton/PauseButton");

    private readonly PauseButtonView _view;
    private readonly ProfilePlayer _profilePlayer;


    public PauseButtonController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.PauseButton.onClick.AddListener(OnButtonPressedListener);
    }

    private void OnButtonPressedListener()
    {
        OnPauseButtonPressed.Invoke();
    }

    protected override void OnDispose()
    {
        _view.PauseButton.onClick.RemoveAllListeners();
        base.OnDispose();
    }


    private PauseButtonView LoadView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
        GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<PauseButtonView>();
    }

}
