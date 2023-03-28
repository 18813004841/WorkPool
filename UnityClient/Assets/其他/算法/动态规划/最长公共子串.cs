using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace 动态规划
{
    public class 最长公共子串
    {
        //给定两个字符串str1和str2,输出两个字符串的最长公共子串
        //题目保证str1和str2的最长公共子串存在且唯一。
        //输入："1AB2345CD","12345EF"  输出："2345"
        public string LCS(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;

            int maxLength = 0;
            int index = -1;
            int[,] dp = new int[m + 1, n + 1];
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i-1] == s2[j-1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                        maxLength = Mathf.Max(maxLength, dp[i, j]);
                        if (maxLength == dp[i, j])
                        {
                            index = i - 1;
                        }
                    }
                    else
                    {
                        dp[i, j] = 0;
                    }

                    if (a动态规划入口.CheckOut())
                    {
                        return "";
                    }
                }
            }

            if (maxLength < 1)
            {
                return "";
            }
            return s1.Substring(index - maxLength + 1, maxLength);
        }
    }
}