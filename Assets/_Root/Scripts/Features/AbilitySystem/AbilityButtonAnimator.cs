using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
internal class AbilityButtonAnimator : MonoBehaviour
{
    [SerializeField] private AbilityButtonAnimatorSettings _settings;

    private Button _button;
    private RectTransform _rectTransform;
    private Vector2 _normalRectTransformSizeDelta;

    private void Awake() => Init();

    private void OnValidate() => Init();

    private void Init()
    {
        _button ??= GetComponent<Button>();
        _rectTransform ??= GetComponent<RectTransform>();
        _normalRectTransformSizeDelta = _rectTransform.sizeDelta;
    }

    private void Start()
    {
        _button.onClick.AddListener(PlayAnimation);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(PlayAnimation);
    }

    private void PlayAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOSizeDelta(_normalRectTransformSizeDelta * _settings.ButtonPressedSize, _settings.Duration));
        sequence.Append(_rectTransform.DOSizeDelta(_normalRectTransformSizeDelta * _settings.ButtonNormalSize, _settings.Duration));
    }
}
