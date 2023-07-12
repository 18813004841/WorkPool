using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS
{
    class Entity_Hero : EntityBase
    {
        public override void OnCreate()
        {
            base.OnCreate();

            switch (World.GameState)
            {
                case EGameState.MainScene:
                    Factory_OnCreateInMainScene();
                    break;
                case EGameState.BattleScene:
                    Factory_OnCreateInBattleScene();
                    break;
            }
        }

        private void Factory_OnCreateInMainScene()
        {
            OnAddComponent(new ComponentMove());
            OnAddComponent(new ComponentMove());
        }

        private void Factory_OnCreateInBattleScene()
        {

        }
    }
}
