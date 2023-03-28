using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace 动态规划
{
    public class 数字字符串转化成IP地址
    {
        //现在有一个只包含数字的字符串，将该字符串转化成IP地址的形式，返回所有可能的情况。
        //输入："25525522135"  输出："["255.255.22.135", "255.255.221.35"]"
        //输入："000256"  输出："[]"
        //输入："1111"  输出："["1.1.1.1"]"

        public string RestoreIpAddresses(string s)
        {
            int m = s.Length;
            int[,,,] dp = new int[3, 3, 3, 3];

            string rt = "";
            List<string> strs = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                if (i + 9 < m-1)
                    continue;
                for (int j = i + 1; j < i + 4 && j < m - 2; j++)
                {
                    if (j + 6 < m - 1)
                    {
                        continue;
                    }
                    for (int k = j + 1; k < j + 4 && k < m - 1; k++)
                    {
                        if (k + 3 < m - 1)
                            continue;
                        Debug.Log($"{i}, {j}, {k}");
                        if (ParseStr(s, i, j, k, m - 1))
                        {
                            strs.Add(s.Insert(k+1, ".").Insert(j + 1, ".").Insert(i + 1, "."));
                        }
                    }
                }
            }

            rt = string.Join("\n", strs);
            return rt;
        }

        private bool ParseStr(string str, int i, int j, int k, int l)
        {
            if (ParseStr(str.Substring(0, i + 1))
                && ParseStr(str.Substring(i + 1, j - i))
                && ParseStr(str.Substring(j + 1, k - j))
                && ParseStr(str.Substring(k + 1, l - k)))
            {
                return true;
            }
            return false;
        }

        private bool ParseStr(string str)
        {
            if (str.StartsWith("0") && str.Length > 1)
            {
                return false;
            }

            return (int.TryParse(str, out var value) && value <= 255);
        }
    }
}