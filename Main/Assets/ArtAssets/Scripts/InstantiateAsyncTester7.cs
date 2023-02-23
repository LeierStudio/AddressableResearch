using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary> 異部加載後實例化 </summary>
public class InstantiateAsyncTester7 : MonoBehaviour
{
    /// <summary> GameObject 操作處理器 </summary>
    List<AsyncOperationHandle<GameObject>> _gameObjectHandles = new List<AsyncOperationHandle<GameObject>>();

    /// <summary> GameObject 們 </summary>
    List<GameObject> _objs = new List<GameObject>();

    /// <summary>
    /// 載入物件
    /// </summary>
    public void LoadGameObject()
    {
        LoadGameObject("Character");
    }

    /// <summary>
    /// 釋放
    /// </summary>
    public void Release()
    {
        var count = _gameObjectHandles.Count;
        for (var i = 0; i < count; i++)
        {
            Debug.Log($"釋放 {_gameObjectHandles[0]}");

            // 在手機上使用此 API 會把 Asset 的 Memony 釋放掉 (UnityEditor 看不出來。) 圖片會消失。
            Addressables.Release(_gameObjectHandles[0]);

            _gameObjectHandles.RemoveAt(0);

            Destroy(_objs[0]);
            _objs.RemoveAt(0);
        }
    }

    /// <summary>
    /// 載入物件
    /// </summary>
    /// <param name="layerName">標籤名稱</param>
    async void LoadGameObject(string layerName)
    {
        var handle = Addressables.LoadResourceLocationsAsync(layerName, typeof(GameObject));
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var item in handle.Result)
            {
                var gameObjectHandle = Addressables.LoadAssetAsync<GameObject>(item);
                _gameObjectHandles.Add(gameObjectHandle);
                await gameObjectHandle.Task;
                var objAsset = gameObjectHandle.Result;
                var obj = Instantiate(objAsset);
                obj.transform.position = GetRandomPos();
                _objs.Add(obj);
                Debug.Log($"產生 {objAsset.name}");
            }
        }
        Addressables.Release(handle);
    }

    /// <summary>
    /// 取得亂數座標
    /// </summary>
    Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0);
    }
}