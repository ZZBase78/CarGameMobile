namespace Features.Shed.Upgrade
{
    internal class FireUpgradeHandler : IUpgradeHandler
    {
        private readonly float _firePower;

        public FireUpgradeHandler(float firePower) =>
            _firePower = firePower;

        public void Upgrade(IUpgradable upgradable) =>
            upgradable.FirePower += _firePower;
    }
}
