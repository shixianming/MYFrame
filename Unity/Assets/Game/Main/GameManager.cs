using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //�༭��ģʽ������YooAsset��Դ����

        //
        ResourcePackage package = YooAssets.GetPackage("DefaultPackage");

        AssetHandle handle = package.LoadAssetAsync<Sprite>("");
    }
}