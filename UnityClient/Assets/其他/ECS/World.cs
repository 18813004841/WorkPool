using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public enum EGameState
    {
        MainScene,
        BattleScene,
    }

    public class World
    {
        private HashSet<SystemBase> _systems;
        private List<EntityBase> _entitys;

        private static EGameState _gameState;
        public static EGameState GameState => _gameState;

        /// <summary>
        /// 切换场景时调用
        /// </summary>
        /// <param name="state"></param>
        public void OnChangeGameState(EGameState state)
        {
            _gameState = state;
            ReBindGameStateSystem();
        }

        #region BindSystem
        /// <summary>
        /// 重新绑定system
        /// </summary>
        private void ReBindGameStateSystem()
        {
            _systems.Clear();

            switch (GameState)
            {
                case EGameState.MainScene:
                    BindSystem_InMainScene();
                    break;
                case EGameState.BattleScene:
                    BindSystem_InBattleScene();
                    break;
            }
        }

        private void BindSystem_InMainScene()
        {
            _systems.Add(new System_Move());
        }

        private void BindSystem_InBattleScene()
        {
            _systems.Add(new System_Move());
            _systems.Add(new System_Attack());
        }
        #endregion
        
        #region Init Entity

        private void InitEntitys()
        {
            for (int i = 0; i < 1000; i++)
            {
                _entitys.Add(new EntityBase());
            }
        }

        #endregion
    }
}
