using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

internal sealed class BundleLoaderView : MonoBehaviour
{
    private bool _isLoading = false;
    private string _webUrl = string.Empty;

    private Action<AssetBundle> _actionOnLoad;

    public void Load(string webUrl, Action<AssetBundle> actionOnLoad)
    {
        if (_isLoading) return;

        _isLoading = true;
        _webUrl = webUrl;

        _actionOnLoad = actionOnLoad;

        StartCoroutine(Download());
    }

    private IEnumerator Download()
    {
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_webUrl);

        yield return request.SendWebRequest();

        while (!request.isDone)
            yield return null;

        CheckRequest(request);
    }

    private void CheckRequest(UnityWebRequest request)
    {
        if (request.error == null)
        {
            AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(request);
            _actionOnLoad?.Invoke(assetBundle);
        }
        else
        {
            _actionOnLoad?.Invoke(null);
        }
    }
}
