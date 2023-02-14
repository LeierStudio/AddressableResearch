using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary> 異部加載後實例化 </summary>
public class InstantiateAsyncTester : MonoBehaviour
{
    [SerializeField] AssetReferenceGameObject Target;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Addressables.InstantiateAsync(Target);
        }
    }
}