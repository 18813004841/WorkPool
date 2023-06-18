using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class ECS_World: ICreate
{
    private ECS_GameStateMatchine _gameStateMatchine;
    public ECS_GameStateMatchine GameStateMatchine => _gameStateMatchine;

    public void Create()
    {
        _gameStateMatchine = new ECS_GameStateMatchine();
        _gameStateMatchine.AddOnChangeStateCallBack(OnChangeStateCallBack);

        _systems.Clear();
    }

    private void OnChangeStateCallBack()
    {
        _systems.Clear();

        switch (_gameStateMatchine.CurStateType)
        {
            case EECSGameState.Valut:
                Factory_CreateInValut();
                break;
            case EECSGameState.Battle:
                Factory_CreateInBattle();
                break;
            default:
                break;
        }
    }

    private void Factory_CreateInValut()
    {

    }

    private void Factory_CreateInBattle()
    {

    }
}