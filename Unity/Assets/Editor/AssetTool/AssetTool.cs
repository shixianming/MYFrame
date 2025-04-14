using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using YooAsset.Editor;

namespace Game.Editor
{
    public static class AssetTool
    {
        public static readonly string ResPath = "Res";

        public static readonly string PackageName = "DefaultPackage";

        /// <summary>
        /// 收集全部资源
        /// </summary>
        [MenuItem("Tools/AssetTool/Collector Assets")]
        public static void CollectorAssets()
        {
            //存在则根据
            Debug.Log(Application.dataPath);

            InitPackage();
        }

        public static void InitPackage()
        {
            Dictionary<AssetBundleCollectorGroup, List<AssetBundleCollector>> allCollectors = new Dictionary<AssetBundleCollectorGroup, List<AssetBundleCollector>>();
            AssetBundleCollectorPackage package = AssetBundleCollectorSettingData.Setting.GetPackage(PackageName);
            if (package == null)
                package = AssetBundleCollectorSettingData.CreatePackage(PackageName);
            List<AssetBundleCollectorGroup> groups = package.Groups;
            foreach (AssetBundleCollectorGroup group in groups) 
            {
                allCollectors[group] = group.Collectors;
                group.ActiveRuleName = "DisableGroup";
            }

            //遍历文件夹
            string rootPath = Path.Combine(Application.dataPath, ResPath);
            DirectoryInfo rootDirectory = new DirectoryInfo(rootPath);
            DirectoryInfo[] topDirectories = rootDirectory.GetDirectories("*", SearchOption.TopDirectoryOnly);
            string[] excludedExtensions = { ".meta" };
            foreach (DirectoryInfo topDirectory in topDirectories)
            {
                FileInfo[] files = topDirectory.GetFiles("*", SearchOption.TopDirectoryOnly)
                    .Where(f => !excludedExtensions.Contains(f.Extension, StringComparer.OrdinalIgnoreCase))
                    .ToArray();
                if (files.Length > 0)
                {
                    Debug.LogError("不规范的文件放置：" + topDirectory.FullName);
                    continue;
                }
                string groupName = topDirectory.Name;
                if (TryGetGroup(allCollectors, groupName, out AssetBundleCollectorGroup group) == false)
                {
                    group = new AssetBundleCollectorGroup();
                    List<AssetBundleCollector> collectors = new List<AssetBundleCollector>();
                    allCollectors[group] = collectors;
                    group.ActiveRuleName = "EnableGroup";
                    group.GroupName = groupName;
                    group.GroupDesc = "";
                    group.AssetTags = "";
                    group.Collectors = collectors;
                    package.Groups.Add(group);
                }
                group.ActiveRuleName = "EnableGroup";
                InitGroup(topDirectory, group.Collectors);
            }
            //删除不符合规则的组
            for (int i = 0; i < groups.Count; i++)
            {
                AssetBundleCollectorGroup group = groups[i];
                if (group.ActiveRuleName == "DisableGroup")
                {
                    AssetBundleCollectorSettingData.RemoveGroup(package, group);
                    i--;
                }
            }
            // 保存配置数据
            AssetBundleCollectorSettingData.ModifyPackage(package);
            AssetBundleCollectorSettingData.SaveFile();
            //更新xml文件
            string resultPath = Path.Combine(Application.dataPath, "Settings/YooAsset");
            AssetBundleCollectorConfig.ExportXmlConfig($"{resultPath}/{nameof(AssetBundleCollectorConfig)}.xml");
        }

        public static bool TryGetGroup(Dictionary<AssetBundleCollectorGroup, List<AssetBundleCollector>> groupDict, string groupName, out AssetBundleCollectorGroup group)
        {
            group = default;
            foreach (AssetBundleCollectorGroup item in groupDict.Keys)
            {
                if (item.GroupName == groupName)
                {
                    group = item;
                    return true;
                }
            }
            return false;
        }


        public static void InitGroup(DirectoryInfo directory, List<AssetBundleCollector> collectors)
        {
            DirectoryInfo[] directories = directory.GetDirectories("*", SearchOption.AllDirectories);
            foreach (DirectoryInfo directoryInfo in directories)
            {
                DirectoryInfo[] subInfos = directoryInfo.GetDirectories("*", SearchOption.AllDirectories);
                FileInfo[] fileInfos = directoryInfo.GetFiles();
                if (subInfos.Length <= 0)
                {
                    string path = GetAssetPath(directoryInfo.FullName);
                    if (collectors.Find(collector => collector.CollectPath == path) == null)
                    {
                        AssetBundleCollector collector = new AssetBundleCollector();
                        collector.CollectPath = path;
                        collector.CollectorGUID = AssetDatabase.AssetPathToGUID(path);
                        collector.CollectorType = ECollectorType.MainAssetCollector;
                        collector.AddressRuleName = "AddressByFileName";
                        collector.PackRuleName = "PackDirectory";
                        collector.FilterRuleName = "CollectAll";
                        collector.UserData = "";
                        collector.AssetTags = "";
                        collectors.Add(collector);
                    }
                }
                else if (subInfos.Length >= 1)
                {
                    string[] excludedExtensions = { ".meta" };
                    FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.TopDirectoryOnly)
                        .Where(f => !excludedExtensions.Contains(f.Extension, StringComparer.OrdinalIgnoreCase))
                        .ToArray();
                    if (files.Length > 0)
                    {
                        Debug.LogWarning("不规范的文件放置：" + directoryInfo.FullName);
                    }
                }
            }
        }

        public static string GetAssetPath(string path)
        {
            path = path.Replace("\\", "/").Replace(Application.dataPath, "");
            path = string.Format("{0}{1}", "Assets", path);
            return path;
        } 
    }
}