using UnityEngine;
using Features.AbilitySystem;

namespace Game.Transport
{
    internal abstract class TransportController : BaseController, IAbilityActivator
    {
        public abstract GameObject ViewGameObject { get; }

        public abstract TransportModel transportModel { get; }
    }
}
