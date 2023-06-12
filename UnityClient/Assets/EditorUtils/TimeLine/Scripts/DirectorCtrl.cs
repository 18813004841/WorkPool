using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorCtrl : MonoBehaviour
{
    public void SetSpeed(float speed)
    {
        Time.timeScale = speed;
    }
}
