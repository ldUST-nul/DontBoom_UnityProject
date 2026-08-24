/// <summary>ゲーム内ステートの基底クラス</summary>
public abstract class GameStates
{
    protected GameStateMachine machine;
    public void SetMachine(GameStateMachine machine) => this.machine = machine;

    public abstract void OnInitialize();
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
