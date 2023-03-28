using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace 动态规划
{
    public class 连续子数组的最大和
    {
        //输入一个长度为n的整型数组array，数组中的一个或连续多个整数组成一个子数组，
        //子数组最小长度为1。求所有子数组的和的最大值。
        //输入：[1,-2,3,10,-4,7,2,-5]  返回值：18
        //输入：[2] 返回值：2
        //输入：[-10] 返回值：-10


        public int FindGreatestSumOfSubArray(List<int> array)
        {
            // write code here
            int[] dp = new int[array.Count];
            int maxValue = array[0];
            dp[0] = array[0];
            for (int i = 1; i < array.Count; i++)
            {
                dp[i] = Mathf.Max(dp[i - 1] + array[i], array[i]);
                maxValue = Mathf.Max(dp[i], maxValue);
            }

            return maxValue;
        }
    }
}