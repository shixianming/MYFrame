using Cysharp.Threading.Tasks;
using YooAsset;
using Object = UnityEngine.Object;


public static class AssetManager
{
    public static string DefaultPackageName = "Default";

    public static void InitAsset()
    {
        YooAssets.Initialize();

    }

    private static ResourcePackage GetPackage(string packageName)
    {
        if (string.IsNullOrEmpty(packageName))
            packageName = DefaultPackageName;
        return YooAssets.GetPackage(packageName);
    }

    public static async UniTask<T> LoadAsync<T>(string path) where T : Object
    {
        return await LoadAsync<T>(path, DefaultPackageName);
    }

    public static async UniTask<T> LoadAsync<T>(string path, string packageName) where T : Object
    {
        AssetHandle handle = GetPackage(packageName).LoadAssetAsync<T>(path);
        await handle.Task;
        return handle.AssetObject as T;
    }

    public static async UniTask<AssetHandle> LoadAsyncWithHandle<T>(string path, string packageName) where T : Object
    {
        AssetHandle handle = GetPackage(packageName).LoadAssetAsync<T>(path);
        await handle.Task;
        return handle;
    }
}