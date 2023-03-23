using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace 动态规划
{
    public class a动态规划入口 : MonoBehaviour
    {
        [Header("死循环检测")]
        public int OutError = 100;
        private int CurStep = 0;

        [Header("输入")]
        public string ParamStr1 = "";

        [ContextMenu("动态规划")]
        void Main()
        {
            最长回文子串 temp = new 最长回文子串();
            int value = temp.GetLength(ParamStr1);
            Debug.Log(value);
        }

        #region 数字转字符串
        //有一种将字母编码成数字的方式：'a'->1, 'b->2', ... , 'z->26'。
        //现在给一串数字，返回有多少种可能的译码结果
        private int Solve(string nums)
        {
            int n = nums.Length;
            int[] dp = new int[n + 1];
            dp[0] = 1;
            dp[1] = nums[0] == '0' ? 0 : 1;
            for (int i = 2; i <= n; i++)
            {
                if (nums[i - 1] != '0')
                {
                    dp[i] += dp[i - 1];
                }
                if (nums[i - 2] == '1' || (nums[i - 2] == '2' && nums[i - 1] <= '6'))
                {
                    dp[i] += dp[i - 2];
                }
            }
            return dp[n];
        }
        #endregion

        #region 找钱
        //给定数组arr，arr中所有的值都为正整数且不重复。
        //每个值代表一种面值的货币，每种面值的货币可以使用任意张，再给定一个aim，
        //代表要找的钱数，求组成aim的最少货币数。如果无解，请返回-1.
        public int MinMoney(List<int> arr, int aim)
        {
            //自己实现的不一定对，测试了两个结果没问题
            if (CheckOut())
            {
                return -1;
            }

            int min = int.MaxValue;
            int value = -1;
            for (int i = 0; i < arr.Count; i++)
            {
                value = aim - arr[i];
                if (value > 0)
                {
                    value = MinMoney(arr, value);
                    if (value != -1)
                    {
                        value += 1;
                        min = Mathf.Min(value, min);
                    }
                }
                else if (value == 0)
                {
                    min = 1;
                }
            }

            if (min != int.MaxValue)
            {
                return min;
            }

            return -1;
        }
        #endregion

        #region 最长上升子序列
        //给定一个长度为 n 的数组 arr，求它的最长严格上升子序列的长度。
        //所谓子序列，指一个数组删掉一些数（也可以不删）之后，形成的新数组。例如[1, 5, 3, 7, 3] 数组，其子序列有：[1,3,3]、[7] 等。但[1, 6]、[1,3,5] 则不是它的子序列。
        //输入：[6,3,1,5,2,3,7]
        //返回值：4
        //说明：该数组最长上升子序列为[1, 2, 3, 7] ，长度为4
        public void CheckChildList()
        {
            int[] arr = ParamStr1.ToIntArray();

            int[] dp = new int[arr.Length + 1];

            int maxLength = 0;

            dp[1] = 1;

            for (int i = 2; i <= arr.Length; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    if (arr[j - 1] < arr[i - 1])
                    {
                        dp[i] = Mathf.Max(dp[i], dp[j]);
                    }
                }
                dp[i] += 1;
                maxLength = Mathf.Max(dp[i], maxLength);
            }

            Debug.Log(maxLength);
        }
        #endregion

        private bool CheckOut()
        {
            CurStep += 1;
            if (CurStep > OutError)
            {
                Debug.LogError("死循环");
            }
            return CurStep > OutError;
        }
    }

    public static class DGTools
    {
        public static int[] ToIntArray(this string str)
        {
            string[] strs = str.Split(',');
            int[] arr = new int[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                arr[i] = int.Parse(strs[i]);
            }
            return arr;
        }
    }
}