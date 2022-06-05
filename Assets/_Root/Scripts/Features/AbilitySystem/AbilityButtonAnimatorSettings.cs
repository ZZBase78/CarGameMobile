using UnityEngine;

[CreateAssetMenu(fileName = nameof(AbilityButtonAnimatorSettings), menuName = "Settings/" + nameof(AbilityButtonAnimatorSettings))]
internal class AbilityButtonAnimatorSettings : ScriptableObject
{
    [field: SerializeField] public float ButtonNormalSize { get; private set; }
    [field: SerializeField] public float ButtonPressedSize { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
}
