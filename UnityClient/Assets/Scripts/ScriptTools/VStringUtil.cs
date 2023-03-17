using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptTools
{
    public class VStringUtil
    {
        /// <summary>
        /// ֻ����Ϊ��ʱ�ַ���ʹ�ã������κεط�ʹ��ֻ�ܸ�ֵ����ʱ���������ɱ���
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
        /// ������ǹ���string���򷵻�str������ǹ���sring�򷵻�copy str
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