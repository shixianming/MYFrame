using YooAsset;
using Cysharp.Threading.Tasks;

namespace Main
{
    public class FsmRequestPackageVersion : FsmBase
    {
        public override void OnCreate(StateMachine machine)
        {
            base.OnCreate(machine);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _ = UpdatePackageVersion();
        }

        public override void OnUpdate()
        {

        }

        public override void OnExit()
        {
            
        }

        private async UniTask UpdatePackageVersion()
        {
            string packageName = (string)_machine.GetBlackboardValue("PackageName");
            ResourcePackage package = YooAssets.GetPackage(packageName);
            RequestPackageVersionOperation operation = package.RequestPackageVersionAsync();
            await operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                Logger.Warning(operation.Error);
                //PatchEventDefine.PackageVersionRequestFailed.SendEventMessage();
            }
            else
            {
                Logger.Log($"Request package version : {operation.PackageVersion}");
                _machine.SetBlackboardValue("PackageVersion", operation.PackageVersion);
                _machine.ChangeState<FsmUpdatePackageManifest>();
            }
        }
    }
}