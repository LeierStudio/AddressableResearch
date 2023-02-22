using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

/// <summary> 異部加載後實例化 </summary>
public class InstantiateAsyncTester5 : MonoBehaviour
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
            Addressables.Release(_operationHandle);
            // 切場景，並不會釋放 Addressable。(需要使用 Addressable 釋放 API 後，在切場景。)
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
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