using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ECS_GameStateMatchine:ICreate
{
    private Dictionary<EECSGameState, ECS_GameStateBase> StatePool = new Dictionary<EECSGameState, ECS_GameStateBase>();

    private EECSGameState _curStateType = EECSGameState.Valut;

    public EECSGameState CurStateType => _curStateType;

    public ECS_GameStateBase CurState => StatePool[_curStateType];

    private Action OnChangeStateCallBack;

    public void Create()
    {
        StatePool.Clear();
        StatePool.Add(EECSGameState.Valut, new ECS_GameState_Valut());
        StatePool.Add(EECSGameState.Valut, new ECS_GameState_Battle());
        OnChangeStateCallBack = OnChangeState;
    }

    public void AddOnChangeStateCallBack(Action onChangeStateCallBack)
    {
        OnChangeStateCallBack += onChangeStateCallBack;
    }

    public void ChangeState(EECSGameState state)
    {
        _curStateType = state;

        OnChangeStateCallBack?.Invoke();
    }

    private void OnChangeState()
    {

    }
}

public enum EECSGameState
{
    Valut,
    Battle
}

public class ECS_GameStateBase
{

}

public class ECS_GameState_Valut: ECS_GameStateBase
{

}

public class ECS_GameState_Battle: ECS_GameStateBase
{

}