using Game.Transport;
using UnityEngine;

namespace Features.AbilitySystem
{
    internal interface IAbilityActivator
    {
        GameObject ViewGameObject { get; }
        TransportModel transportModel { get; }
    }
}
