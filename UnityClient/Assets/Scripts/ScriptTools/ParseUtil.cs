using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace ScriptTools
{
    public class ParseUtil
    {
        public const float Const_Float_DefaultValue = 0f;
        public const int Const_Int_DefaultValue = 0;
        public const byte Const_Byte_DefaultValue = 0;

        #region ParseFloat

        public static float ParseFloat(string str)
        {
            return ParseFloat(str, Const_Float_DefaultValue);
        }

        public static float ParseFloat(string str, float defaultValue)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defaultValue;
            }

            if (float.TryParse(str, out var rt))
            {
                return rt;
            }
            return defaultValue;
        }

        #endregion

        #region ParseInt

        public static int ParseInt(string str)
        {
            return ParseInt(str, Const_Int_DefaultValue);
        }

        public static int ParseInt(string str, int defaultValue)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defaultValue;
            }

            if (int.TryParse(str, out var rt))
            {
                return rt;
            }
            else
            {
                if (str.Contains("0x"))
                {
                    try
                    {
                        return Convert.ToInt32(str, 16);
                    }
                    catch (Exception)
                    {
                        return defaultValue;
                    }
                }
            }
            return defaultValue;
        }

        #endregion

        #region ParseByte

        public static byte ParseByte(string str)
        {
            return ParseByte(str, Const_Byte_DefaultValue);
        }

        public static byte ParseByte(string str, byte defaultValue)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defaultValue;
            }

            if (byte.TryParse(str, out var rt))
            {
                return rt;
            }
            else
            {
                if (str.Contains("0x"))
                {
                    try
                    {
                        return Convert.ToByte(str, 16);
                    }
                    catch (Exception)
                    {

                        return defaultValue;
                    }
                }
            }

            return defaultValue;
        }

        #endregion

        #region ParseVector

        public static Vector2 ParseVector2(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return Vector2.zero;
            }

            Vector2 rt = Vector2.zero;

            string[] array = str.Split(',');
            if (array.Length >= 1)
                rt.x = ParseFloat(array[0]);
            if (array.Length >= 2)
                rt.y = ParseFloat(array[1]);

            return rt;
        }

        public static Vector2 ParseVector3(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return Vector3.zero;
            }

            Vector3 rt = Vector3.zero;

            string[] array = str.Split(',');
            if (array.Length >= 1)
                rt.x = ParseFloat(array[0]);
            if (array.Length >= 2)
                rt.y = ParseFloat(array[1]);
            if (array.Length >= 3)
                rt.y = ParseFloat(array[2]);

            return rt;
        }

        public static Vector4 ParseVector4(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return Vector4.zero;
            }

            string[] array = str.Split(',');

            Vector4 rt = Vector4.zero;

            if (array.Length >= 1)
                rt.x = ParseFloat(array[0]);
            if (array.Length >= 2)
                rt.y = ParseFloat(array[1]);
            if (array.Length >= 3)
                rt.y = ParseFloat(array[2]);
            if (array.Length >= 4)
                rt.y = ParseFloat(array[3]);

            return rt;
        }

        #endregion

    }
}