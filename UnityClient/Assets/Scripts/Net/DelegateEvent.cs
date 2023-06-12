using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelegateEvent : MonoBehaviour
{
    public delegate void Dele();
    public Dele MyDele;
    public UnityEvent MyEvent;
    public UnityEvent<bool> MyEvent2;
    public Action MyAction;
    public Func<Type> MyFunc;

    private void Awake()
    {
        MainDele();
        //MainEvent();
    }

    [ContextMenu("MainDele")]
    public void MainDele()
    {
        MyDele = null;
        MyDele += Fun1;
        MyDele += Fun2;
        MyDele += Fun2;
        MyDele -= Fun2;
        MyDele += Fun3;
        MyDele();
    }

    [ContextMenu("MainEvent")]
    public void MainEvent()
    {
        MyEvent.RemoveAllListeners();
        MyEvent.AddListener(Fun1);
        MyEvent.AddListener(Fun2);
        MyEvent.AddListener(Fun2);
        MyEvent.RemoveListener(Fun2);
        MyEvent.AddListener(Fun3);
        MyEvent.Invoke();
    }

    public void Fun1()
    {
        for (int i = 0; i < 10000000; i++)
        {
            for (int j = 0; j < 100; j++)
            {

            }
        }

        Debug.Log("Fun1");
    }

    public void Fun2()
    {
        for (int i = 0; i < 10000; i++)
        {

        }
        Debug.Log("Fun2");
    }

    public void Fun3()
    {
        for (int i = 0; i < 10; i++)
        {

        }
        Debug.Log("Fun3");
    }

    public void Fun4(bool bl)
    {
        for (int i = 0; i < 10; i++)
        {

        }
        Debug.Log("Fun4");
    }

    public void Debug1()
    {
        Debug.Log("Fun1");
    }

    public void Debug2()
    {
        Debug.Log("Fun2");
    }

    public void Debug3()
    {
        Debug.Log("Fun3");
    }

    public void Debug4()
    {
        Debug.Log("Fun4");
    }
}
