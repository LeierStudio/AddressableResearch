using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary> �����[�����Ҥ� </summary>
public class InstantiateAsyncTester2 : MonoBehaviour
{
    /// <summary> �ؼ� </summary>
    [SerializeField] AssetReferenceGameObject Target;

    /// <summary> �ؼЪ��� (Inspector �w����) </summary>
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
            // �i�H���� Addressable �귽�C
            Addressables.ReleaseInstance(_targetObj);
        }
    }

    void OnLoad(AsyncOperationHandle<GameObject> obj)
    {
        _targetObj = obj.Result;
    }
}