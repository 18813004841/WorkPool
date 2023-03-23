using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace 动态规划
{
    public class 最长回文子串 : MonoBehaviour
    {
        //对于长度为n的一个字符串A（仅包含数字，大小写英文字母），
        //请设计一个高效算法，计算其中最长回文子串的长度。
        //输入："ababc"    返回值：3
        //输入："abbba"    返回值：5
        //输入："b"    返回值：1

        public int GetLength(string str)
        {
            int n = str.Length;
            int ans = 0;
            for (int i = 0; i < n; i++)
            {
                // 以 i 为中心的回文串
                int l = i, r = i;
                while (l >= 0 && r < n && str[l] == str[r])
                {
                    l--;
                    r++;
                }
                ans = Mathf.Max(ans, r - l - 1);
                // 以 i 和 i+1 为中心的回文串
                l = i;
                r = i + 1;
                while (l >= 0 && r < n && str[l] == str[r])
                {
                    l--;
                    r++;
                }
                ans = Mathf.Max(ans, r - l - 1);
            }
            return ans;
        }

        //Manacher 算法，原理是将原本字符串字符中插入新字符，使整个字符串变为必须是奇数回文串
        public int GetLengthInManacher(string s)
        {
            StringBuilder t = new StringBuilder();
            t.Append('^');
            foreach (char c in s)
            {
                t.Append('#');
                t.Append(c);
            }
            t.Append("#$");
            int n = t.Length;
            int[] P = new int[n];
            int C = 0, R = 0, ans = 0;
            for (int i = 1; i < n - 1; i++)
            {
                int i_mirror = 2 * C - i;
                P[i] = (R > i) ? Mathf.Min(R - i, P[i_mirror]) : 0;
                while (t[i + P[i] + 1] == t[i - P[i] - 1])
                {
                    P[i]++;
                }
                if (i + P[i] > R)
                {
                    C = i;
                    R = i + P[i];
                }
                ans = Mathf.Max(ans, P[i]);
            }
            return ans;
        }
    }
}