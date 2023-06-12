using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSharpBase : MonoBehaviour
{
    [ContextMenu("Run")]
    public void Main()
    {
        Score score1 = new Score();
        Score.SetScore(ref score1);
        Score score2 = new Score();
        Score.SetScore(ref score2);
        Debug.LogError((score1.Value + score2.Value).ToString());
    }

    public struct Score
    {
        public int Value;

        public static void SetScore(ref Score score)
        {
            score.Value = 100;
        }
    }
}
