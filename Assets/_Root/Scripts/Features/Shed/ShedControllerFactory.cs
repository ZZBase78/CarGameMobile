using Features.Inventory;
using Features.Inventory.Items;
using Features.Shed;
using Features.Shed.Upgrade;
using Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

internal sealed class ShedControllerFactory
{
    private ProfilePlayer _profilePlayer;
    private Transform _placeForUI;

    private ShedController _shedController;
    private GameObject _shedViewGameObject;
    private InventoryController _inventoryController;
    private GameObject _inventoryShedGameObject;
    private ItemsRepository _itemsRepository;
    private IReadOnlyDictionary<string, IUpgradeHandler> _upgradeItems;

    public ShedControllerFactory(ProfilePlayer profilePlayer, Transform placeForUI)
    {
        _profilePlayer = profilePlayer;
        _placeForUI = placeForUI;
    }

    public void Display(Action<IReadOnlyDictionary<string, IUpgradeHandler>> actionOnDone, Action actionOnBack)
    {
        CreateInventoryController();
        var shedView = LoadShedView();
        _upgradeItems = CreateUpgradeItems();
        _shedController = new ShedController(_profilePlayer.CurrentTransport, shedView);
        _shedController.OnDone += () => actionOnDone.Invoke(_upgradeItems);
        _shedController.OnBack += actionOnBack;
    }

    public void Destroy()
    {
        _inventoryController?.Dispose();
        _itemsRepository?.Dispose();
        _shedController?.Dispose();

        GameObject.Destroy(_shedViewGameObject);
        GameObject.Destroy(_inventoryShedGameObject);
    }

    private ShedView LoadShedView()
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(new ResourcePath("Prefabs/Shed/ShedView"));
        _shedViewGameObject = GameObject.Instantiate(prefab, _placeForUI, false);

        return _shedViewGameObject.GetComponent<ShedView>();
    }

    private IReadOnlyDictionary<string, IUpgradeHandler> CreateUpgradeItems()
    {
        UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource"));
        var repository = new UpgradeHandlersRepository(upgradeConfigs);
        return repository.Items;
    }

    private void CreateInventoryController()
    {
        InventoryView inventoryView = LoadInventoryView(_placeForUI);
        InventoryModel inventoryModel = _profilePlayer.Inventory;
        CreateItemsRepository();
        _inventoryController = new InventoryController(inventoryView, inventoryModel, _itemsRepository);
    }

    private InventoryView LoadInventoryView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(new ResourcePath("Prefabs/Inventory/InventoryView"));
        _inventoryShedGameObject = GameObject.Instantiate(prefab, placeForUi);
        return _inventoryShedGameObject.GetComponent<InventoryView>();
    }

    private void CreateItemsRepository()
    {
        var path = new ResourcePath("Configs/Inventory/ItemConfigDataSource");

        ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(path);
        _itemsRepository = new ItemsRepository(itemConfigs);
    }
}
