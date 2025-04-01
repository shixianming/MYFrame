using YooAsset;

namespace Main
{
    public class FsmClearCacheBundle : FsmBase
    {
        public override void OnCreate(StateMachine machine)
        {
            base.OnCreate(machine);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            string packageName = (string)_machine.GetBlackboardValue("PackageName");
            ResourcePackage package = YooAssets.GetPackage(packageName);
            ClearCacheFilesOperation operation = package.ClearCacheFilesAsync(EFileClearMode.ClearUnusedBundleFiles);
            operation.Completed += Operation_Completed;
        }

        public override void OnUpdate()
        {
        }

        public override void OnExit()
        {
        }

        private void Operation_Completed(AsyncOperationBase op)
        {
            _machine.ChangeState<FsmPackageReady>();
        }
    }
}