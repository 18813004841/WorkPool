using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public class EntityBase
    {
        private Action<ComponentBase> _onAddComponentCallBack;
        public Action<ComponentBase> OnAddComponentCallBack => _onAddComponentCallBack;

        private Dictionary<Type, ComponentBase> _componentDic = new Dictionary<Type, ComponentBase>();

        public virtual void OnCreate()
        {

        }

        protected virtual void OnAddComponent(ComponentBase componentBase)
        {
            Type typeKey = componentBase.GetType();
            if (!_componentDic.ContainsKey(typeKey))
            {
                _componentDic.Add(typeKey, componentBase);
            }
        }

        public void Create(Action<ComponentBase> onAddComponentCallBack)
        {
            _onAddComponentCallBack = OnAddComponent;
            _onAddComponentCallBack += onAddComponentCallBack;
        }
    }
}
