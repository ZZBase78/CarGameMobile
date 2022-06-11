using UnityEngine;
using UnityEngine.UI;

internal sealed class BundleTestUIView : MonoBehaviour
{
    private const string BACKGROUND_BUTTON_ASSET_NAME = "box_exp2";

    [SerializeField] private Button _buttonLoad;
    [SerializeField] private Button _buttonExit;

    [SerializeField] private Image _backgroundImage;

    void Start()
    {
        _buttonLoad.onClick.AddListener(ButtonLoadPressed);
        _buttonExit.onClick.AddListener(ButtonExitPressed);
    }

    private void ButtonLoadPressed()
    {
        _buttonLoad.interactable = false;
        string webUrl = new GoogleDriveDownloadUrl().GenerateUrl(GoogleDriveIDs.TEST_BUNDLE);
        new BundleLoadController(webUrl, OnBundleLoad).Load();
    }

    private void OnBundleLoad(AssetBundle assetBundle)
    {
        if (assetBundle == null)
        {
            Debug.Log("Bundle not loaded");
            return;
        }
        _backgroundImage.sprite = assetBundle.LoadAsset<Sprite>(BACKGROUND_BUTTON_ASSET_NAME);
        assetBundle.Unload(false);
    }

    private void ButtonExitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void OnDestroy()
    {
        _buttonLoad.onClick.RemoveAllListeners();
        _buttonExit.onClick.RemoveAllListeners();
    }

}
