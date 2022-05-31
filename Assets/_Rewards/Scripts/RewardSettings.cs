using Rewards;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RewardSettings), menuName = "Settings/" + nameof(RewardSettings))]
internal sealed class RewardSettings : ScriptableObject
{
    [Header("Frequency")]
    public FrequencyType frequencyType;

    [Header("Settings Time Get Reward")]
    public float TimeCooldown;
    public float TimeDeadline;

    [Header("Settings Rewards")]
    public List<Reward> Rewards;
}
