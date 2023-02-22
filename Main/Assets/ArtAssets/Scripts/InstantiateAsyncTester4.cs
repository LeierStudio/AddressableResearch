using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary> 異部加載後實例化 </summary>
public class InstantiateAsyncTester4 : MonoBehaviour
{
    /// <summary> 目標 </summary>
    [SerializeField] AssetReference Target;

    /// <summary> 目標物件們 </summary>
    List<GameObject> _targetObjs = new List<GameObject>();

    /// <summary> 操作處理器 </summary>
    AsyncOperationHandle<GameObject> _operationHandle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 這個寫法不好釋放 Addressable。
            for (var i = 0; i < 5; i++)
            {
                Addressables.InstantiateAsync(Target);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // 這個寫法比較好釋放 Addressable。
            _operationHandle = Addressables.LoadAssetAsync<GameObject>(Target);
            _operationHandle.Completed += OnLoadFinished;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (var targetObj in _targetObjs)
            {
                Destroy(targetObj);
            }
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
        for (var i = 0; i < 5; i++)
        {
            _targetObjs.Add(Instantiate(obj.Result));
        }
    }
}