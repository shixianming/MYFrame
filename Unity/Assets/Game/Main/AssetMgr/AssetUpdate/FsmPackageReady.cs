namespace Game.Runtime
{
    public class FsmPackageReady : FsmBase
    {
        private AssetOperation _owner;

        public override void OnCreate(StateMachine machine)
        {
            base.OnCreate(machine);
            _owner = _machine.Owner as AssetOperation;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _owner.SetFinish();
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}