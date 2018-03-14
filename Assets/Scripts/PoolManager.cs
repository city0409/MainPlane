using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UObject = UnityEngine.Object;

public class PoolManager : PersistentSingleton<PoolManager> 
{
    private AssetBundle manifestBundle;
    private AssetBundleManifest manifest;
    
	protected override  void Awake() 
	{
        base.Awake();
	}

    public void CreateByBundle(string objName,string bundleName,Action<UObject> func)
    {
        CreateByBundleName(objName, bundleName, func);
    }
	
	private void CreateByBundleName(string objName,string bundleName,Action<UObject> func) 
	{
        if (manifest == null)
        {
            manifestBundle = AssetBundle.LoadFromFile(Path.Combine(AppConst.DataPath, "StreamingAssers"));
            manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");
        }

        string[] depends = manifest.GetAllDependencies(bundleName);
        for (int index = 0; index < depends.Length; index++)
        {
            //StartCoroutine()
        }
	}
}
