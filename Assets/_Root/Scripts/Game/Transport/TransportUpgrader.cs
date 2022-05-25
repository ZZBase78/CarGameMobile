using Features.Shed.Upgrade;
using System;
using System.Collections.Generic;

internal sealed class TransportUpgrader : IDisposable
{
    private IUpgradable _upgradable;
    private IReadOnlyList<string> _equippedItems;
    private IReadOnlyDictionary<string, IUpgradeHandler> _upgradeItems;
    private UpgradeHandlersRepository _upgradeHandlersRepository;

    public TransportUpgrader(IUpgradable upgradable, IReadOnlyList<string> equippedItems)
    {
        _upgradable = upgradable;
        _equippedItems = equippedItems;
        _upgradeHandlersRepository = new UpgradeHandlerRepositoryFactory().Create();
        _upgradeItems = _upgradeHandlersRepository.Items; 
    }

    public void Upgrade()
    {
        _upgradable.Restore();

        foreach (string itemId in _equippedItems)
            if (_upgradeItems.TryGetValue(itemId, out IUpgradeHandler handler))
                handler.Upgrade(_upgradable);
    }

    public void Dispose()
    {
        _upgradeHandlersRepository?.Dispose();
    }
}