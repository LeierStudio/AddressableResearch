using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary> �����[�����Ҥ� </summary>
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