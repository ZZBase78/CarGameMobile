using Game;
using Profile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameInitialSettings), menuName = "Settings/" + nameof(GameInitialSettings))]
internal sealed class GameInitialSettings : ScriptableObject
{
    public float SpeedCar;
    public GameState InitialState;
    public TransportType TransportType;

}
