using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal sealed class BundleLoadController
{
    private const string BUNDLE_LOADER_NAME = "BundleLoader";

    private string _webUrl;
    private Action<AssetBundle> _actionOnLoad;
    private GameObject _bundleLoader;

    public BundleLoadController(string webUrl, Action<AssetBundle> actionOnLoad)
    {
        _webUrl = webUrl;
        _actionOnLoad = actionOnLoad;
    }

    public void Load()
    {
        _bundleLoader = new GameObject(BUNDLE_LOADER_NAME);
        BundleLoaderView bundleLoaderView = _bundleLoader.AddComponent<BundleLoaderView>();
        bundleLoaderView.Load(_webUrl, OnBundleLoad);
    }

    private void OnBundleLoad(AssetBundle assetBundle)
    {
        GameObject.Destroy(_bundleLoader);
        _actionOnLoad?.Invoke(assetBundle);
    }
}
