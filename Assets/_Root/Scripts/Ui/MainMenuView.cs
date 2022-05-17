using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonRewarded;
        [SerializeField] private Button _buttonBuyItem;


        public void Init(UnityAction startGame, UnityAction rewardedAction, UnityAction buyItemAction)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonRewarded.onClick.AddListener(rewardedAction);
            _buttonBuyItem.onClick.AddListener(buyItemAction);
        }

        public void OnDestroy() =>
            _buttonStart.onClick.RemoveAllListeners();
    }
}
