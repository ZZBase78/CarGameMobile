using Ui;
using Tool;
using Game;
using Profile;
using UnityEngine;
using System;
using System.Collections.Generic;
using Features.Shed;
using Features.Shed.Upgrade;
using Features.Inventory;
using Object = UnityEngine.Object;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private readonly List<GameObject> _subObjects = new List<GameObject>();
    private readonly List<IDisposable> _subDisposables = new List<IDisposable>();

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private ShedController _shedController;
    private GameController _gameController;


    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        DisposeControllers();
        DisposeSubInstances();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        DisposeControllers();
        DisposeSubInstances();

        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _shedController = CreateShedController(_profilePlayer, _placeForUi);
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer);
                break;
        }
    }

    private void DisposeControllers()
    {
        _mainMenuController?.Dispose();
        _settingsMenuController?.Dispose();
        _shedController?.Dispose();
        _gameController?.Dispose();
    }

    private void DisposeSubInstances()
    {
        DisposeSubDisposables();
        DisposeSubObjects();
    }

    private void DisposeSubDisposables()
    {
        foreach (IDisposable disposable in _subDisposables)
            disposable.Dispose();

        _subDisposables.Clear();
    }

    private void DisposeSubObjects()
    {
        foreach (GameObject gameObject in _subObjects)
            Object.Destroy(gameObject);

        _subObjects.Clear();
    }


    private ShedController CreateShedController(ProfilePlayer profilePlayer, Transform placeForUi)
    {
        InventoryMvcContainer inventoryMvcContainer = CreateInventoryContainer(profilePlayer.Inventory, placeForUi);
        UpgradeHandlersRepository shedRepository = CreateShedRepository();
        ShedView shedView = LoadShedView(placeForUi);
        // чтобы представление гаража оказалось "выше" представления инвентаря
        // (иначе инвентарь будет блокировать кнопки)
        // нужно загрузить представления гаража только после инвентаря

        return new ShedController
        (
            shedView,
            profilePlayer,
            inventoryMvcContainer,
            shedRepository
        );
    }

    private UpgradeHandlersRepository CreateShedRepository()
    {
        var path = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");

        UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(path);
        var repository = new UpgradeHandlersRepository(upgradeConfigs);
        _subDisposables.Add(repository);

        return repository;
    }

    private ShedView LoadShedView(Transform placeForUi)
    {
        var path = new ResourcePath("Prefabs/Shed/ShedView");

        GameObject prefab = ResourcesLoader.LoadPrefab(path);
        GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
        _subObjects.Add(objectView);

        return objectView.GetComponent<ShedView>();
    }

    private InventoryMvcContainer CreateInventoryContainer(IInventoryModel model, Transform placeForUi)
    {
        var container = new InventoryMvcContainer(model, placeForUi);
        _subDisposables.Add(container);

        return container;
    }
}
