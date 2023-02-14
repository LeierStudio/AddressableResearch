using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary> 異部加載後實例化 </summary>
public class InstantiateAsyncTester2 : MonoBehaviour
{
    /// <summary> 目標 </summary>
    [SerializeField] AssetReferenceGameObject Target;

    /// <summary> 目標物件 (Inspector 預覽用) </summary>
    [SerializeField] GameObject _targetObj;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Addressables.InstantiateAsync(Target).Completed += OnLoad;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Destroy(_targetObj);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Destroy(_targetObj);
            // 可以釋放 Addressable 資源。
            Addressables.ReleaseInstance(_targetObj);
        }
    }

    void OnLoad(AsyncOperationHandle<GameObject> obj)
    {
        _targetObj = obj.Result;
    }
}