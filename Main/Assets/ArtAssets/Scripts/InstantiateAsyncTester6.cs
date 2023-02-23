using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary> 異部加載後實例化 </summary>
public class InstantiateAsyncTester6 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(LoadLayerGameObjectByCoroutine("Character"));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            LoadGameObjectByLayerByAsyncAwait("Character");
        }
    }

    /// <summary>
    /// 載入該標籤的物件 (Coroutine)
    /// </summary>
    IEnumerator LoadLayerGameObjectByCoroutine(string layerName)
    {
        // 搜尋標籤是 Character 的 GameObject 的任何物件
        //var handle = Addressables.LoadResourceLocationsAsync(layerName);

        // 搜尋標籤是 Character 的 GameObject 物件
        var handle = Addressables.LoadResourceLocationsAsync(layerName, typeof(GameObject));
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var item in handle.Result)
            {
                var gameObjectHandle = Addressables.LoadAssetAsync<GameObject>(item);
                yield return gameObjectHandle;
                var objAsset = gameObjectHandle.Result;
                var obj = Instantiate(objAsset);
                obj.transform.position = GetRandomPos();
                Debug.Log($"Coroutine 產生 {objAsset.name}");
                Addressables.Release(gameObjectHandle);
            }
        }
        Addressables.Release(handle);
    }

    /// <summary>
    /// 載入該標籤的物件 (async await)
    /// </summary>
    async void LoadGameObjectByLayerByAsyncAwait(string layerName)
    {
        // 搜尋標籤是 Character 的 GameObject 的任何物件
        //var handle = Addressables.LoadResourceLocationsAsync(layerName);

        // 搜尋標籤是 Character 的 GameObject 物件
        var handle = Addressables.LoadResourceLocationsAsync(layerName, typeof(GameObject));
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var item in handle.Result)
            {
                var gameObjectHandle = Addressables.LoadAssetAsync<GameObject>(item);
                await gameObjectHandle.Task;
                var objAsset = gameObjectHandle.Result;
                var obj = Instantiate(objAsset);
                obj.transform.position = GetRandomPos();
                Debug.Log($"Async await 產生 {objAsset.name}");
                Addressables.Release(gameObjectHandle);
            }
        }
        Addressables.Release(handle);
    }

    Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0);
    }
}