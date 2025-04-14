using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //编辑器模式不启用YooAsset资源管理

        //
        ResourcePackage package = YooAssets.GetPackage("DefaultPackage");

        AssetHandle handle = package.LoadAssetAsync<Sprite>("");
    }
}