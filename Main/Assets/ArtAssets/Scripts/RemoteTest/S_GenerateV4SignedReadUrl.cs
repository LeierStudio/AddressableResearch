using Google.Cloud.Storage.V1;
using System;
using System.IO;
using System.Net.Http;
using UnityEngine;

public class S_GenerateV4SignedReadUrl : MonoBehaviour
{
    public string BucketName;

    readonly TextAsset _key;

    public string Func_GetSignedURL(string objectPathOnCloud)
    {
        var signURL = GenerateV4SignedReadUrl(BucketName, objectPathOnCloud);
        return signURL;
    }

    public string GenerateV4SignedReadUrl(string bucketName = "your-unique-bucket-name", string objectName = "your-object-name")
    {
        var path = $"{Application.persistentDataPath}/Credentical.json";
        File.WriteAllText(path, $"{_key}");

        var urlSigner = UrlSigner.FromServiceAccountPath(path);
        var url = urlSigner.Sign(bucketName, objectName, TimeSpan.FromSeconds(50), HttpMethod.Get);
        File.WriteAllText(path, "");
        return url;
    }
}
