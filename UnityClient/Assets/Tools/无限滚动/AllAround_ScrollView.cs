using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllAround_ScrollView : MonoBehaviour
{
    [Header("ScrollRect component")]
    public ScrollRect Scroll;
    [Header("是否使用水平滑动")]
    public bool UseHorizontal;
    [Header("是否使用垂直滑动")]
    public bool UseVertical;
    [Header("是否使用滚动条")]
    public bool UseScrollBar;

    [Header("水平滚动条")]
    public GameObject HorizontalBar;
}
