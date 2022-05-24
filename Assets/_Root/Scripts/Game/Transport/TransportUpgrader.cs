using Features.Shed.Upgrade;
using System.Collections.Generic;

internal sealed class TransportUpgrader
{
    private IUpgradable _upgradable;
    private IReadOnlyList<string> _equippedItems;

    public TransportUpgrader(IUpgradable upgradable, IReadOnlyList<string> equippedItems)
    {
        _upgradable = upgradable;
        _equippedItems = equippedItems;
    }

    public void Upgrade(IReadOnlyDictionary<string, IUpgradeHandler> upgradeItems)
    {
        _upgradable.Restore();

        foreach (string itemId in _equippedItems)
            if (upgradeItems.TryGetValue(itemId, out IUpgradeHandler handler))
                handler.Upgrade(_upgradable);
    }
}
