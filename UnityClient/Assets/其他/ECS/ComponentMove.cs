using UnityEngine;

namespace ECS
{
    class ComponentMove:ComponentBase
    {
        public EntityBase TargetEntity;
        public Vector3 TargetPos;
    }
}
