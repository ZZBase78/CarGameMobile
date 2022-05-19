using Tool;
using Game;
using Game.Transport;
using Features.Inventory;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly TransportModel CurrentTransport;
        public readonly InventoryModel Inventory;


        public ProfilePlayer(float transportSpeed, float jumpHeight, TransportType transportType, GameState initialState)
        {
            CurrentState = new SubscriptionProperty<GameState>(initialState);
            CurrentTransport = new TransportModel(transportSpeed, jumpHeight, transportType);
            Inventory = new InventoryModel();
        }
    }
}
