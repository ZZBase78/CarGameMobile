using Features.Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

internal sealed class ItemsRepositoryFactory
{
    private readonly ResourcePath _path = new ResourcePath("Configs/Inventory/ItemConfigDataSource");

    public ItemsRepository Create()
    {
        ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(_path);
        return new ItemsRepository(itemConfigs);
    }
}
