using System.Collections.Generic;
using System.Threading;

public abstract class AsyncDataBase
{

}

public abstract class AsyncBase:System.IDisposable
{
    protected Thread thread;
    protected ManualResetEvent manualResetEvent = new ManualResetEvent(false);
    protected object locker = new object();
    protected List<AsyncDataBase> taskList = new List<AsyncDataBase>();

    protected void StartThread()
    {
        if (null == thread)
        {
            thread = new Thread(new ThreadStart(ThreadMainFunc))
            {
                Priority = ThreadPriority.Normal,
            };
            thread.Start();
        }
        manualResetEvent.Set();
    }

    protected abstract void ThreadMainFunc();

    protected abstract bool TaskParameterCheck(AsyncDataBase data);

    /// <summary>
    /// 结束线程【不会立刻退出】
    /// </summary>
    public void StopThread()
    {
        lock (locker)
        {
            if (taskList.Count > 0)
            {
                UnityEngine.Debug.LogError("StopThread left count " + taskList.Count);
            }
            taskList.Clear();
            manualResetEvent.Reset();
        }
    }

    /// <summary>
    /// 获取剩余未执行完的任务数量
    /// </summary>
    /// <returns></returns>
    public int GetLeftTaskCount()
    {
        lock (locker)
        {
            return taskList.Count;
        }
    }

    public int GetThreadState()
    {
        if (null != thread)
        {
            return (int)thread.ThreadState;
        }
        return -1;
    }

    public void AddTask(AsyncDataBase data)
    {
        if (!TaskParameterCheck(data))
        {
            return;
        }

        lock (locker)
        {
            taskList.Add(data);
            StartThread();
        }
    }

    public void Dispose()
    {
        manualResetEvent.Close();
    }
}
