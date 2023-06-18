using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ECS_World : ManagerSingleton<ECS_World>
{
    private List<ECS_SystemBase> _systems = new List<ECS_SystemBase>();
    public List<ECS_SystemBase> Systems => _systems;
}