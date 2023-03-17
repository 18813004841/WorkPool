using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptTools
{
    public class VStringUtil
    {
        /// <summary>
        /// 只能作为临时字符串使用，代码任何地方使用只能赋值给临时变量，不可保存
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string Concat(params string[] strs)
        {
            VString vString = VStringShareObject.GetShareVString();
            vString.Concat(true, strs);
            return vString.GetString();
        }

        /// <summary>
        /// 如果不是共享string，则返回str，如果是共享sring则返回copy str
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToNormalString(string str)
        {
            if (VStringShareObject.UseShareObject(str) || VString.UseShareObject(str))
            {
                return string.Copy(str);
            }
            return str;
        }
    }
}