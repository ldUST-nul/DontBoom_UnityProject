/// <summary>ステートを切り替えるためのマシン</summary>
public class GameStateMachine
{
    /// <summary>ステート切り替え用変数</summary>
    internal GameStates CurrentState { get; private set; }

    internal GameStateMachine(GameStates state)
    {
        TransitionTo(state);
    }
    /// <summary>状態切替</summary>
    /// <param name="state">変更対象ステート</param>
    internal void TransitionTo(GameStates state)
    {
        CurrentState?.OnExit();
        CurrentState = state;
        CurrentState.SetMachine(this);
        CurrentState.OnEnter();
    }
    /// <summary>ゲーム毎フレーム呼起</summary>
    internal void OnUpdate()
    {
        CurrentState?.OnUpdate();
    }
}
