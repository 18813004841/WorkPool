using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace 动态规划
{
    public class 最长公共子序列
    {
        //给定两个字符串str1和str2，输出两个字符串的最长公共子序列。
        //如果最长公共子序列为空，则返回"-1"。目前给出的数据，仅仅会存在一个最长的公共子序列
        //输入："1A2C3D4B56","B1D23A456A"  输出："123456"
        //输入："abc","def"    输出："-1"
        //输入："abc","abc"    输出："abc"
        //输入："ab",""        输出："-1"
        public string LCS(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;

            //1.求长度
            int[,] dp = new int[m + 1, n + 1];
            for (int i = 1; i < m + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Mathf.Max(dp[i - 1, j], dp[i, j - 1]);
                    }

                    if (a动态规划入口.CheckOut())
                    {
                        return "";
                    }
                }
            }
            //2.判断长度
            if (dp[m, n] <= 0)
            {
                return "-1";
            }

            //3.找到字串
            StringBuilder str = new StringBuilder();
            while (m > 0 && n > 0)
            {
                if (s1[m - 1] == s2[n - 1])
                {
                    str.Insert(0, s1[m - 1]);
                    m--;
                    n--;
                }
                else if (dp[m-1, n] >= dp[m,n-1])
                {
                    m--;
                }
                else
                {
                    n--;
                }

                if (a动态规划入口.CheckOut())
                {
                    return "";
                }
            }

            return str.ToString();
        }
    }
}