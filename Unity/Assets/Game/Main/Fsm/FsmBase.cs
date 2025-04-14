namespace Game.Runtime
{
    public class FsmBase : IStateNode
    {
        protected StateMachine _machine;

        public virtual void OnCreate(StateMachine machine)
        {
            _machine = machine;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}