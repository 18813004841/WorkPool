using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class 动态规划 : MonoBehaviour
{
    public List<int> _arr;
    public int aim;

    [Header("死循环检测")]
    public int OutError = 100;
    private int CurStep = 0;

    [ContextMenu("动态规划")]
    void Main()
    {
        CurStep = 0;
        Debug.Log(MinMoney(_arr, aim));
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
            else if(value == 0)
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
