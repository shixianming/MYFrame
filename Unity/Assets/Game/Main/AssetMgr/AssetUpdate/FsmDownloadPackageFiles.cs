using YooAsset;
using Cysharp.Threading.Tasks;

namespace Main
{
    public class FsmDownloadPackageFiles : FsmBase
    {
        public override void OnCreate(StateMachine machine)
        {
            base.OnCreate(machine);
        }

        public override void OnEnter()
        {
            _ = BeginDownload();
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
            
        }

        private async UniTask BeginDownload()
        {
            ResourceDownloaderOperation downloader = (ResourceDownloaderOperation)_machine.GetBlackboardValue("Downloader");
            //downloader.DownloadErrorCallback = PatchEventDefine.WebFileDownloadFailed.SendEventMessage;
            //downloader.DownloadUpdateCallback = PatchEventDefine.DownloadUpdate.SendEventMessage;
            downloader.BeginDownload();
            await downloader;

            // 检测下载结果
            if (downloader.Status != EOperationStatus.Succeed)
                return;

            _machine.ChangeState<FsmDownloadPackageOver>();
        }
    }
}
