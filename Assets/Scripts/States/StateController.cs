using Cysharp.Threading.Tasks;

public class StateController
{
    protected StateMachine StateMachine;

    public void SetMachine(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public virtual async UniTask EnterState()
    {

    }

    public virtual async UniTask ExitState()
    {

    }

    protected async UniTask GoTo<T>() where T : StateController
    {
        await StateMachine.ChangeState<T>();
    }
}
