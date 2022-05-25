using Features.Shed.Upgrade;
using Tool;

internal sealed class UpgradeHandlerRepositoryFactory
{
    private readonly ResourcePath _path = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");

    public UpgradeHandlersRepository Create()
    {
        UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_path);
        var repository = new UpgradeHandlersRepository(upgradeConfigs);
        return repository;
    }
}
