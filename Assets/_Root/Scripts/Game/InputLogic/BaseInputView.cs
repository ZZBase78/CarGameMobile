using Tool;
using UnityEngine;
using JoostenProductions;

namespace Game.InputLogic
{
    internal abstract class BaseInputView : MonoBehaviour
    {
        private SubscriptionProperty<float> _leftMove;
        private SubscriptionProperty<float> _rightMove;
        protected float _speed;
        private bool _active;


        private void Start() =>
            UpdateManager.SubscribeToUpdate(CheckAndMove);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(CheckAndMove);


        public virtual void Init(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            float speed)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            _speed = speed;
            _active = true;
        }

        public void SetActive(bool active) => _active = active;

        private void CheckAndMove()
        {
            if (_active) Move();
        }


        protected abstract void Move();

        protected virtual void OnLeftMove(float value) =>
            _leftMove.Value = value;

        protected virtual void OnRightMove(float value) =>
            _rightMove.Value = value;
    }
}
