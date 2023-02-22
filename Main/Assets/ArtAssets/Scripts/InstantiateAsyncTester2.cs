using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary> 異部加載後實例化 </summary>
public class InstantiateAsyncTester2 : MonoBehaviour
{
    /// <summary> 目標 </summary>
    [SerializeField] AssetReferenceGameObject Target;

    /// <summary> 目標物件 </summary>
    GameObject _targetObj;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Addressables.InstantiateAsync(Target).Completed += OnLoadFinished;
        }
        // 錯誤
        if (Input.GetKeyDown(KeyCode.S))
        {
            // 純刪物件，Addressable 不會釋放。
            Destroy(_targetObj);
        }
        // 正確
        if (Input.GetKeyDown(KeyCode.D))
        {
            Destroy(_targetObj);
            // 釋放此 GameObject 相關的 Addressable。
            Addressables.ReleaseInstance(_targetObj);
        }
    }

    /// <summary>
    /// [事件] 載入完成
    /// </summary>
    /// <param name="obj">物件</param>
    void OnLoadFinished(AsyncOperationHandle<GameObject> obj)
    {
        _targetObj = obj.Result;
    }
}