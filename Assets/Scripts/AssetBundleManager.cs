using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManager : MonoBehaviour 
{
    //private static AssetBundle GetAssetBundle(string url,int version)
    //{
    //    string keyName = url + version.ToString();
    //    AssetBundle abRef;

    //    if (dictAssetBundleRefs.TryGetValua(keyName, out abRef))
    //        return abRef.assetBundle;
    //    else
    //        return null;
    //}

    //public static IEnumerator DownloadAssetBundle(string url ,int version, Action<UObject> func)
    //{
    //    while (isLoading)
    //    {
    //        yield return null;
    //    }
    //    string keyName = url + version.ToString();
    //    if (dictAssetBundleRefs.ContainsKey(keyName))
    //    {
    //        if (func != null)
    //            func(GetAssetBundle(url, version));
    //    }
    //    else
    //    {
    //        idLoading = true;
    //        AssetBundleRef abRef = new AssetBundleRef(url, version);

    //        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(url);
    //        while (!request .isDone )
    //        {
    //            yield return null;
    //        }
    //        abRef.asserBundle = request.assetBundle;
    //        if (func != null)
    //            func(request.assetBundle);
    //        dictAssetBundleRefs.Add(keyName, abRef);
    //        isLoading = false;

    //    }
    //}
}
