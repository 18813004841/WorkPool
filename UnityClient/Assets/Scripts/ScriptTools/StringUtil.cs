using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ScriptTools
{
    public class StringUtil
    {
        //自定义字符串函数公用的StringBuilder
        static StringBuilder _customSB = new StringBuilder();
        //共享的StringBuilder
        static StringBuilder shareSB = new StringBuilder();

        /// <summary>
        /// 字符串拼接
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string Concat(params string[] strs)
        {
            _customSB.Remove(0, _customSB.Length);

            if (null == strs)
            {
                return _customSB.ToString();
            }

            for (int i = 0; i < strs.Length; i++)
            {
                _customSB.Append(strs[i]);
            }

            return _customSB.ToString();
        }

        public static StringBuilder GetShareStringBuilder(bool bReset = true)
        {
            if (bReset)
            {
                shareSB.Remove(0, shareSB.Length);
            }
            return shareSB;
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="format"></param>
        /// <param name="ags"></param>
        /// <returns></returns>
        public static string Format(string format, params object[] args)
        {
            try
            {
                _customSB.Remove(0, _customSB.Length);
                _customSB.AppendFormat(format, args);
                return _customSB.ToString();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
        }

        /// <summary>
        /// 替换\\n 为\n
        /// </summary>
        /// <param name="baseStr"></param>
        /// <returns></returns>
        public static string ReplaceNewLineChar(string baseStr)
        {
            return baseStr.Replace("\\n", "n");
        }

        /// <summary>
        /// 替换转义字符
        /// </summary>
        /// <param name="baseStr"></param>
        /// <returns></returns>
        public static  string ReplaceTranslateChar(string baseStr)
        {
            baseStr = baseStr.Replace("\\n", "\n");
            baseStr = baseStr.Replace("\\t", "\t");
            baseStr = baseStr.Replace("\\b", " ");
            return baseStr;
        }

        /// <summary>
        /// 替换\\s 为（全角）空格
        /// </summary>
        /// <param name="baseStr"></param>
        /// <returns></returns>
        public static string ReplaceNewBlankSpaceChar(string baseStr)
        {
            return baseStr.Replace("\\s", " ");
        }

        //正则匹配空格规则
        static System.Text.RegularExpressions.Regex SpaceRgx = null;
        private static string GetSpacePattern()
        {
            return "\\s(?![a-z]\\s)";
        }

        public static System.Text.RegularExpressions.Regex GetSpaceRgx()
        {
            if (null == SpaceRgx)
            {
                SpaceRgx = new System.Text.RegularExpressions.Regex(GetSpacePattern(), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }
            return SpaceRgx;
        }

        //处理英文混排时空格换行的问题
        public static string ProSpace(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            return ProSpaceSp(value);
        }

        public static string ProSpaceSp(string value)
        {
            return value.Replace("{sp}", "\u00A0");
        }

        public static string ProSpaceNormal(string value)
        {
            return GetSpaceRgx().Replace(value, "\u00A0");
        }

        /// <summary>
        /// 文本加持颜色
        /// </summary>
        /// <param name="color"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UITextColor(string color, string text)
        {
            return StringUtil.Format("<color=#{0}>{1}</color>", color, text);
        }

        /// <summary>
        /// 文本加持颜色
        /// </summary>
        /// <param name="color"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UITextColor(Color color, string text)
        {
            return UITextColor(ColorUtility.ToHtmlStringRGBA(color), text);
        }

        /// <summary>
        /// 整数转字符串
        /// </summary>
        /// <param name="num"></param>
        /// <param name="limit"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string Int2StrigLimit(int num, int limit, string param = "")
        {
            if (num < limit)
            {
                return num.ToString(param);
            }
            else
            {
                return limit.ToString(param);
            }
        }

        /// <summary>
        /// 替换为win的斜杠
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringSlashOfWin(string str)
        {
            return str.Replace('/', '\\');
        }

        /// <summary>
        /// 服务器接收的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToServerSafeString(string str)
        {
            return str.Replace("|", "/");
        }

        public static string DictionaryConvertString(Dictionary<int,int> dic)
        {
            _customSB.Remove(0, _customSB.Length);
            if (dic != null)
            {
                Dictionary<int, int>.Enumerator itor = dic.GetEnumerator();
                while (itor.MoveNext())
                {
                    _customSB.Append(itor.Current.Key);
                    _customSB.Append(",");
                    _customSB.Append(itor.Current.Value);
                    _customSB.Append(";");
                }
            }
            return _customSB.ToString();
        }

        #region 美式数字

        public static string Num2US(float num)
        {
            return num.ToString("n2").Replace(',', ' ');
        }

        public static string Num2US(int num)
        {
            return num.ToString("n0").Replace(',', ' ');
        }

        public static string Num2US(long num)
        {
            return num.ToString("n0").Replace(',', ' ');
        }

        #endregion
    }

    public static class TempStringUtil
    {
        private static volatile object lockThis = new object();

        public static string ToTempString(this int i)
        {
            lock (lockThis)
            {
                return VString.IntToString(i);
            }
        }

        public static string ToTempString(this float f, int digits = 2)
        {
            lock (lockThis)
            {
                return VString.FloatToString(f);
            }
        }

        public static string ToTempString(this long l)
        {
            lock (lockThis)
            {
                return VString.LongToString(l);
            }
        }

        public static string ToTemoStringUpper(this string str)
        {
            lock (lockThis)
            {
                return VString.ToUpper(str);
            }
        }

        public static  string ToTempSubString(this string str,int index, int count)
        {
            lock (lockThis)
            {
                return VString.ToTempSubString(str, index, count);
            }
        }

        #region 转美式字符
        public static string ToStringUS(this float f)
        {
            return StringUtil.Num2US(f);
        }

        public static string ToStringUS(this int f)
        {
            return StringUtil.Num2US(f);
        }

        public static string ToStringUS(this long f)
        {
            return StringUtil.Num2US(f);
        }
        #endregion

    }

}