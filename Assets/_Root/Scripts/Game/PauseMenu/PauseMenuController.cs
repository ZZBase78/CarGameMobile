using Profile;
using System;
using Tool;
using UnityEngine;

internal class PauseMenuController : BaseController
{
    public event Action OnButtonContinuePressed = delegate { };
    public event Action OnButtonSettingsPressed = delegate { };
    public event Action OnButtonExitPressed = delegate { };

    private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/PauseMenu/PauseMenu");

    private readonly PauseMenuView _view;
    private readonly ProfilePlayer _profilePlayer;


    public PauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.ButtonContinue.onClick.AddListener(ButtonContinuePressed);
        _view.ButtonSettings.onClick.AddListener(ButtonSettingsPressed);
        _view.ButtonExit.onClick.AddListener(ButtonExitPressed);
    }

    private void ButtonContinuePressed()
    {
        OnButtonContinuePressed.Invoke();
    }

    private void ButtonSettingsPressed()
    {
        OnButtonSettingsPressed.Invoke();
    }

    private void ButtonExitPressed()
    {
        OnButtonExitPressed.Invoke();
    }

    protected override void OnDispose()
    {
        _view.ButtonContinue.onClick.RemoveAllListeners();
        _view.ButtonSettings.onClick.RemoveAllListeners();
        _view.ButtonExit.onClick.RemoveAllListeners();
        base.OnDispose();
    }


    private PauseMenuView LoadView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
        GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<PauseMenuView>();
    }

}
