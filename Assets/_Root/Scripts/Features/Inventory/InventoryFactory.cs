using Features.Inventory;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

internal sealed class InventoryFactory
{
    private const string PATH = "Prefabs/Inventory/InventoryView";

    public InventoryView CreateView(Transform placeForUi)
    {
        GameObject gameObject = new GameObjectInstantiator().CreateGameObject(PATH, placeForUi);
        return gameObject.GetComponent<InventoryView>();
    }
}
