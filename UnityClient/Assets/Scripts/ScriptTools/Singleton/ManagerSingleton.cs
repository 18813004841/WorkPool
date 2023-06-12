using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSingleton<T> where T : class, new()
{
    private static T _instance;

    public static T Innstance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new T();
            }
            return _instance;
        }
    }

    public static void Release()
    {
        _instance = null;
    }
}
