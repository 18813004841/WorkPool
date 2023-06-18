using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECS_EntityBase:ICreate
{
    private Dictionary<Type, ECS_ComponentBase> _components = new Dictionary<Type, ECS_ComponentBase>();
    public Dictionary<Type, ECS_ComponentBase> Components => _components;

    private Action<ECS_ComponentBase> _onAddComponentCallBack;


    public void Create()
    {
        _components.Clear();

        OnCreate();
    }

    protected virtual void OnCreate()
    {
        
    }

    public void SetOnAddComponentCallBack(Action<ECS_ComponentBase> callback)
    {
        _onAddComponentCallBack = callback;
    }

    public bool AddComponent(ECS_ComponentBase component)
    {
        Type type = typeof(Component);
        if (_components.ContainsKey(type))
        {
            return false;
        }

        _components.Add(type, component);
        return true;
    }

    public bool TryGetComponent<T>(out ECS_ComponentBase component) where T : ECS_ComponentBase
    {
        Type type = typeof(T);
        return _components.TryGetValue(type, out component);
    }
}
