using UnityEngine;
using UnityEngine.UI;

internal class PauseMenuView : MonoBehaviour
{
    [field: SerializeField] public Button ButtonContinue {get; private set;}
    [field: SerializeField] public Button ButtonSettings {get; private set;}
    [field: SerializeField] public Button ButtonExit {get; private set;}
}
