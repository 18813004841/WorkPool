namespace ScriptTools
{
    /// <summary>
    /// ���ݿɱ���ַ���
    /// ������ֻ��������ʱ����ʹ�ã����Բ��������߼��й�洢���ã�����VString�ͷ���string����
    /// </summary>
    public class VString
    {
        private string _data;
        private int maxCount;
        private static int _internalVsIndex;
        private static VString[] _internalVSArray = new VString[]
        {
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
            new VString(64),
        };
        private static string[] digitalNumberArray = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public VString(int maxCount = 1024)
        {
            this.maxCount = maxCount + 1; //����Զ�㣬������1�����ַ���������
            _data = new string('\0', this.maxCount);
            Clear();
        }

        public string GetString()
        {
            return _data;
        }

        /// <summary>
        /// intתstring����GC��ע�����ɵ�stringһ�����ܽ��д洢
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string IntToString(int val)
        {
            return LongToString(val);
        }

        /// <summary>
        /// longתstring����GC��ע�����ɵ�stringһ�����ܽ��д洢
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string LongToString(long val)
        {
            if (val == 0)
            {
                return "0";
            }

            VString tempVS = GetInternalVString();
            bool isNegative = false;
            if (val < 0)
            {
                val = -val;
                isNegative = true;
            }

            while (val != 0)
            {
                long mod = val % 10;
                val = val / 10;
                tempVS.Push(digitalNumberArray[mod]);
            }

            if (isNegative)
            {
                tempVS.Push("-");
            }

            tempVS.ReverseString();
            return tempVS.GetString();
        }

        /// <summary>
        /// floatתstring����GC��ע�����ɵ�stringһ�����ܽ��д洢
        /// </summary>
        /// <param name="f"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static string FloatToString(float f, int digits = 2)
        {
            bool isNegative = false;
            if (f < 0)
            {
                f = -f;
                isNegative = true;
            }

            int iPart = UnityEngine.Mathf.FloorToInt(f);
            float fPart = f - iPart;

            VString tempVSO = GetInternalVString();

            if (iPart != 0)
            {
                while (iPart != 0)
                {
                    long mod = iPart % 10;
                    iPart = iPart / 10;
                    tempVSO.Push(digitalNumberArray[mod]);
                }
            }
            else
            {
                tempVSO.Push("0");
            }

            if (isNegative)
            {
                tempVSO.Push("-");
            }
            tempVSO.ReverseString();

            if (digits != 0)
            {
                VString tempVS1 = GetInternalVString();
                fPart = fPart * UnityEngine.Mathf.Pow(10, digits);
                int iPart2 = UnityEngine.Mathf.RoundToInt(fPart);

                int i = 0;
                while (iPart2 != 0 && i < digits)
                {
                    long mod = iPart2 % 10;
                    iPart2 = iPart2 / 10;
                    i++;
                    tempVS1.Push(digitalNumberArray[mod]);
                }
                tempVS1.ReverseString();

                tempVSO.Push(".");
                tempVSO.Push(tempVS1.GetString());
                while (i < digits)
                {
                    i++;
                    tempVSO.Push("0");
                }
            }
            else
            {
                tempVSO.Push(".");
                for (int i = 0; i < digits; i++)
                {
                    tempVSO.Push("0");
                }
            }

            return tempVSO.GetString();
        }

        /// <summary>
        /// ��һ���ַ���������ת��Ϊlower case��ע�����ɵ�stringһ�����ܽ��д洢
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToLower(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                VString tempVS = VStringShareObject.GetShareVString();
                tempVS.Push(str);
                tempVS.ToLower();
                return tempVS.GetString();
            }
            return str;
        }

        /// <summary>
        /// ��һ���ַ���������ת����upper case��ע�����ɵ�stringһ�����ܽ��д洢
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUpper(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                VString tempVS = VStringShareObject.GetShareVString();
                tempVS.Push(str);
                tempVS.ToUpper();
                return tempVS.GetString();
            }
            return str;
        }

        public static string ToTempSubString(string str, int index, int count)
        {
            if (string.IsNullOrEmpty(str) || count <= 0 || index < 0)
            {
                UnityEngine.Debug.LogError(string.Concat("ToTempSubString IsNullOrEmpty", index.ToString(), "/", count.ToString()));
                return str;
            }

            if (index + count > str.Length)
            {
                UnityEngine.Debug.LogError(string.Concat("ToTempSubString", str, index.ToString(), "/", count.ToString()));
            }

            VString tempVS1 = VStringShareObject.GetShareVString();
            tempVS1.Push(str);
            VString tempVS2 = VStringShareObject.GetShareVString();
            tempVS2.CopyFrom(tempVS1, index, count);
            return tempVS2.GetString();
        }

        public string Concat(bool clear = true, params string[] strs)
        {
            if (clear)
            {
                Clear();
            }

            if (strs == null)
            {
                return _data;
            }

            for (int i = 0; i < strs.Length; i++)
            {
                Push(strs[i]);
            }
            return _data;
        }

        public static bool UseShareObject(string str)
        {
            for (int i = 0; i < _internalVSArray.Length; i++)
            {
                if (string.ReferenceEquals(str, _internalVSArray[i].GetString()))
                {
                    return true;
                }
            }
            return false;
        }

        public unsafe void Push(string newStr)
        {
            if (string.IsNullOrEmpty(newStr))
            {
                return;
            }

            int copyLen = newStr.Length;
            int newLen = _data.Length + copyLen;
            if ((newLen + 1) > maxCount) //���Զ����ַ���������
            {
                int len = newLen;
                copyLen = maxCount - _data.Length - 1;
                newLen = maxCount - 1;//�����µĳ���
                UnityEngine.Debug.LogError(string.Concat("�����������ӳ���", maxCount.ToString(), " ", len.ToString()));
            }

            if (copyLen <= 0)
            {
                return;
            }

            fixed (char* src = newStr)
            {
                fixed (char* dst = _data)
                {
                    UnsafeUtil.memcpyimpl((byte*)src, (byte*)(dst + _data.Length), copyLen * 2);    //system.string�Ĵ洢ÿ��Ԫ�������ֽ�

                    int* iDst = (int*)dst;
                    iDst = iDst - 1;    //�ַ����ĺ����ڵ�һ��Ԫ�ص�ǰ��4���ֽ�
                    *iDst = newLen;

                    char* iEnd = (char*)(dst + newLen);
                    *iEnd = (char)0;    //�����ַ���������
                }
            }
        }

        public unsafe void Clear()
        {
            fixed (char* p = _data)
            {
                int* pSize = (int*)p;
                pSize = pSize - 1;
                *pSize = 0;
            }
        }

        public unsafe void CopyFrom(VString srcVstring, int starIndex, int count)
        {
            if ((count + 1) > maxCount) //��1���ַ���������
            {
                throw new System.ArgumentException(string.Concat("copy count is larger then maxCount",
                    count.ToString(), " ", maxCount.ToString()));
            }

            string srcStr = srcVstring.GetString();
            if (starIndex + count > srcStr.Length)
            {
                throw new System.ArgumentException(string.Concat("copy count is larger then srcString len",
                    count.ToString(), " ", srcStr.Length.ToString(), " ", starIndex.ToString()));
            }

            Clear();

            fixed (char* src = srcStr)
            {
                fixed (char* dst = _data)
                {
                    UnsafeUtil.memcpyimpl((byte*)(src + starIndex), (byte*)dst, count * 2); //system.string�Ĵ洢ÿ��Ԫ�������ֽ�

                    int* iDst = (int*)dst;
                    iDst = iDst - 1;    //�ַ����ĳ����ڵ�һ��Ԫ�ص�ǰ��4���ֽ�
                    *iDst = count;

                    char* iEnd = (char*)(dst + _data.Length);
                    *iEnd = (char)0;    //�����ַ���������
                }
            }
        }

        public unsafe void ToLower()
        {
            int index = 0;
            int len = _data.Length;
            fixed (char* dst = _data)
            {
                while (index < len)
                {
                    char tempChar = *(dst + index);
                    *(dst + index) = char.ToLower(tempChar);
                    ++index;
                }
            }
        }

        public unsafe void ToUpper()
        {
            int index = 0;
            int len = _data.Length;
            fixed (char* dst = _data)
            {
                while (index < len)
                {
                    char tempChar = *(dst + index);
                    *(dst + index) = char.ToUpper(tempChar);
                    ++index;
                }
            }
        }

        private unsafe string ReverseString()
        {
            int len = _data.Length;
            if (len > 0)
            {
                fixed (char* pHead = _data)
                {
                    int count = len / 2;
                    for (int i = 0; i < count; i++)
                    {
                        char temp = pHead[i];
                        pHead[i] = pHead[len - 1 - i];
                        pHead[len - 1 - i] = temp;
                    }
                }
            }
            return _data;
        }

        private static VString GetInternalVString()
        {
            _internalVsIndex = (_internalVsIndex + 1) % _internalVSArray.Length;
            VString vString = _internalVSArray[_internalVsIndex];
            vString.Clear();
            return vString;
        }
    }

    public static class VStringShareObject
    {
        private static volatile object lockThis = new object();
        private static int _internalVSIndex;
        private static VString[] _internalVSArray = new VString[]
        {
            new VString(2048),
            new VString(2048),
            new VString(2048),
            new VString(2048),
            new VString(2048),
            new VString(2048),
            new VString(2048),
            new VString(2048),
            new VString(2048),
            new VString(2048),
            new VString(2048),
            new VString(2048),
            new VString(2048),
            new VString(2048),
            new VString(2048),
        };

        public static VString GetShareVString()
        {
            lock (lockThis)
            {
                _internalVSIndex = (_internalVSIndex + 1) % _internalVSArray.Length;
                VString vString = _internalVSArray[_internalVSIndex];
                vString.Clear();
                return vString;
            }
        }

        public static bool UseShareObject(string str)
        {
            for (int i = 0; i < _internalVSArray.Length; i++)
            {
                if (string.ReferenceEquals(str, _internalVSArray[i].GetString()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}