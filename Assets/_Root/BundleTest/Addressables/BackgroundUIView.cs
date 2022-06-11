using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

internal sealed class BackgroundUIView : MonoBehaviour
{
    [SerializeField] private Button _buttonAddBackground;
    [SerializeField] private Button _buttonRemoveBackground;

    [SerializeField] private AssetReference _backgroundSprite;
    [SerializeField] private Image _backgroundImage;
    private AsyncOperationHandle<Sprite> _sprite;

    private void Start()
    {
        _buttonAddBackground.onClick.AddListener(AddBackgroundPressed);
        _buttonRemoveBackground.onClick.AddListener(RemoveBackgroundPressed);
    }

    private void AddBackgroundPressed()
    {
        _sprite = Addressables.LoadAssetAsync<Sprite>(_backgroundSprite);
        _sprite.Completed += OnBackgroudLoadComlete;
    }

    private void OnBackgroudLoadComlete(AsyncOperationHandle<Sprite> sprite)
    {
        _sprite.Completed -= OnBackgroudLoadComlete;
        _backgroundImage.sprite = sprite.Result;
    }

    private void RemoveBackgroundPressed()
    {
        Addressables.Release(_backgroundImage.sprite);
        _backgroundImage.sprite = null;
    }

    private void OnDestroy()
    {
        _buttonAddBackground.onClick.RemoveAllListeners();
        _buttonRemoveBackground.onClick.RemoveAllListeners();
    }
}
