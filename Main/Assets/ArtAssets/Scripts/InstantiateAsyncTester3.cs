using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary> 異部加載後實例化 </summary>
public class InstantiateAsyncTester3 : MonoBehaviour
{
    /// <summary> 目標 </summary>
    [SerializeField] AssetReference Target;

    /// <summary> 目標物件 </summary>
    GameObject _targetObj;

    /// <summary> 操作處理器 </summary>
    AsyncOperationHandle<GameObject> _operationHandle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _operationHandle = Addressables.LoadAssetAsync<GameObject>(Target);
            _operationHandle.Completed += OnLoadFinished;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Destroy(_targetObj);
            // 釋放 AsyncOperationHandle。
            Addressables.Release(_operationHandle);
        }
    }

    /// <summary>
    /// [事件] 載入完成
    /// </summary>
    /// <param name="obj">物件</param>
    void OnLoadFinished(AsyncOperationHandle<GameObject> obj)
    {
        _targetObj = Instantiate(obj.Result);
    }
}