namespace Main
{
    public class FsmDownloadPackageOver : FsmBase
    {
        public override void OnCreate(StateMachine machine)
        {
            base.OnCreate(machine);
        }

        public override void OnEnter()
        {
            // 资源文件下载完毕
            _machine.ChangeState<FsmClearCacheBundle>();
        }

        public override void OnUpdate()
        {
        }

        public override void OnExit()
        {
        }
    }
}