using System;
using UnityEngine;
using UnityEngine.UI;

internal sealed class TestSpeedUiView : MonoBehaviour
{
    [SerializeField] Button _buttonTestSpeed;

    private DateTime _startLoad;
    private DateTime _finishLoad;

    private void Start()
    {
        _buttonTestSpeed.onClick.AddListener(ButtonTestSpeedPressed);
        _startLoad = DateTime.UtcNow;
        _finishLoad = DateTime.UtcNow;
    }

    private void ButtonTestSpeedPressed()
    {
        _startLoad = DateTime.UtcNow;
        Debug.Log($"Start time: {_startLoad}");
        new BundleLoadController(new GoogleDriveDownloadUrl().GenerateUrl(GoogleDriveIDs.SPEED_TEST_NO_COMPRESSION), OnBundleLoad).Load();

        //LZMA - Duration time: 00:00:02.6160078
        //LZ4 - Duration time: 00:00:02.2933325
        //NO COMPRESSION - Duration time: 00:00:02.6240092
    }

    private void OnBundleLoad(AssetBundle assetBundle)
    {
        if (assetBundle == null)
        {
            Debug.Log("Bundle not loaded");
            return;
        }
        _finishLoad = DateTime.UtcNow;
        Debug.Log($"End time: {_finishLoad}");
        Debug.Log($"Duration time: {_finishLoad - _startLoad}");
    }

    private void OnDestroy()
    {
        _buttonTestSpeed.onClick.RemoveAllListeners();
    }
}
