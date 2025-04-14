using YooAsset;

namespace Game.Runtime
{
    public class FsmCreateDownloader : FsmBase
    {
        public override void OnCreate(StateMachine machine)
        {
            base.OnCreate(machine);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            CreateDownloader();
        }

        public override void OnUpdate()
        {

        }

        public override void OnExit()
        {

        }

        private void CreateDownloader()
        {
            string packageName = (string)_machine.GetBlackboardValue("PackageName");
            ResourcePackage package = YooAssets.GetPackage(packageName);
            int downloadingMaxNum = 10;
            int failedTryAgain = 3;
            var downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
            _machine.SetBlackboardValue("Downloader", downloader);

            if (downloader.TotalDownloadCount == 0)
            {
                Logger.Log("Not found any download files !");
                _machine.ChangeState<FsmPackageReady>();
            }
            else
            {
                // 发现新更新文件后，挂起流程系统
                // 注意：开发者需要在下载前检测磁盘空间不足
                int totalDownloadCount = downloader.TotalDownloadCount;
                long totalDownloadBytes = downloader.TotalDownloadBytes;
                //TODO--[sxm]提示更新文件数量和大小
                //PatchEventDefine.FoundUpdateFiles.SendEventMessage(totalDownloadCount, totalDownloadBytes);
            }
        }
    }
}