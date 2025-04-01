using YooAsset;

namespace Main
{
    public class AssetOperation : GameAsyncOperation
    {
        private readonly StateMachine _machine;
        private readonly string _packageName;

        public AssetOperation(string packageName, EPlayMode playMode)
        {
            _packageName = packageName;

            //创建状态机
            _machine = new StateMachine(this);
            _machine.AddNode<FsmInitializePackage>();
            _machine.AddNode<FsmRequestPackageVersion>();
            _machine.AddNode<FsmUpdatePackageManifest>();
            _machine.AddNode<FsmCreateDownloader>();
            _machine.AddNode<FsmDownloadPackageFiles>();
            _machine.AddNode<FsmDownloadPackageOver>();
            _machine.AddNode<FsmClearCacheBundle>();
            _machine.AddNode<FsmPackageReady>();

            _machine.SetBlackboardValue("PackageName", packageName);
            _machine.SetBlackboardValue("PlayMode", playMode);
        }

        protected override void OnStart()
        {
            _machine.Run<FsmInitializePackage>();
        }

        protected override void OnUpdate()
        {
            
        }

        protected override void OnAbort()
        {
            
        }

        public void SetFinish()
        {
            //异步操作完成
            Status = EOperationStatus.Succeed;
            Logger.Log($"Package {_packageName} patch done !");
        }
    }
}