using YooAsset;
using Cysharp.Threading.Tasks;

namespace Main
{
    public class FsmUpdatePackageManifest : FsmBase
    {
        public override void OnCreate(StateMachine machine)
        {
            base.OnCreate(machine);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _ = UpdateManifest();
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
            
        }

        private async UniTask UpdateManifest()
        {
            string packageName = (string)_machine.GetBlackboardValue("PackageName");
            string packageVersion = (string)_machine.GetBlackboardValue("PackageVersion");
            ResourcePackage package = YooAssets.GetPackage(packageName);
            UpdatePackageManifestOperation operation = package.UpdatePackageManifestAsync(packageVersion);
            await operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                Logger.Warning(operation.Error);
                //PatchEventDefine.PackageManifestUpdateFailed.SendEventMessage();
                return;
            }
            else
            {
                _machine.ChangeState<FsmCreateDownloader>();
            }
        }
    }
}