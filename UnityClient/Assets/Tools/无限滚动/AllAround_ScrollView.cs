using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllAround_ScrollView : MonoBehaviour
{
    [Header("ScrollRect component")]
    public ScrollRect Scroll;
    [Header("�Ƿ�ʹ��ˮƽ����")]
    public bool UseHorizontal;
    [Header("�Ƿ�ʹ�ô�ֱ����")]
    public bool UseVertical;
    [Header("�Ƿ�ʹ�ù�����")]
    public bool UseScrollBar;

    [Header("ˮƽ������")]
    public GameObject HorizontalBar;
}
