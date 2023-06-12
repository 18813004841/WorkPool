using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 异步的解压一个目录下的zip到另外一个目录
/// </summary>
public class AsyncDirUnZipFile : AsyncBase
{
    public class Data : AsyncDataBase
    {
        public string zipSrcDir;
        public string unzipDstDir;
        public HashSet<string> ignoreExtensions;
        public int dealCount;
        public int totalCount;
        public int realUnzipCount;
        public HashSet<string> unzipFileNames;

        public Data(string zipSrcDir, string unzipDstDir, HashSet<string> ignoreExtensions)
        {
            this.zipSrcDir = zipSrcDir;
            this.unzipDstDir = unzipDstDir;
            this.ignoreExtensions = ignoreExtensions;
            this.dealCount = 0;
            this.totalCount = 0;
            this.realUnzipCount = 0;
            this.unzipFileNames = new HashSet<string>();
        }
    }

    protected override bool TaskParameterCheck(AsyncDataBase data)
    {
        Data realData = (Data)data;
        if (null == realData || string.IsNullOrEmpty(realData.zipSrcDir) ||
            string.IsNullOrEmpty(realData.unzipDstDir))
        {
            Debug.LogError("AsyncDirUnZipFile task params error");
            return false;
        }
        return true;
    }

    protected override void ThreadMainFunc()
    {
        throw new System.NotImplementedException();
    }

}
