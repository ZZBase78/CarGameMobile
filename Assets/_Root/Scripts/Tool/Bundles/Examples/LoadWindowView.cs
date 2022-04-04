using UnityEngine;
using UnityEngine.UI;

namespace Tool.Bundles.Examples
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [SerializeField] private Button _loadAssetsButton;

        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAsset);
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
        }

        private void LoadAsset()
        {
            _loadAssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetAssetBundles());
        }
    }
}
