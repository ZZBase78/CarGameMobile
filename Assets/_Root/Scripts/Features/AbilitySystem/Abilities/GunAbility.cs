using System;
using UnityEngine;
using JetBrains.Annotations;
using Object = UnityEngine.Object;
using Tool;

namespace Features.AbilitySystem.Abilities
{
    internal class GunAbility : IAbility
    {
        private const string ADDED_FIRE = "Prefabs/Ability/RocketBomb";
        private const float TIME_TO_DESTROY = 5f;
        private readonly AbilityItemConfig _config;
        private GameObject AddedFirePrefab;


        public GunAbility([NotNull] AbilityItemConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            AddedFirePrefab = ResourcesLoader.LoadPrefab(new ResourcePath(ADDED_FIRE));
        }

        public GameObject GetFirePrefab(IAbilityActivator activator)
        {
            if (activator.transportModel.FirePower <= 1f)
            {
                return _config.Projectile;
            }
            else
            {
                return AddedFirePrefab;
            }
        }

        public void Apply(IAbilityActivator activator)
        {
            var firePrefab = GetFirePrefab(activator);
            var projectile = Object.Instantiate(firePrefab).GetComponent<Rigidbody2D>();
            Vector3 force = activator.ViewGameObject.transform.right * _config.Value;
            projectile.AddForce(force, ForceMode2D.Force);
            GameObject.Destroy(projectile.gameObject, TIME_TO_DESTROY);
        }
    }
}
