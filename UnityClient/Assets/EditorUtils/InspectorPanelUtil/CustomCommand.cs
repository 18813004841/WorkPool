using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace EditorUtils
{

    [AddComponentMenu("Custom/CustomCommand")] //可以在菜单栏Component内添加组件按钮
    //[CanEditMultipleObjects] //允许当选择多个挂有该脚本的对象时，统一修改值，这里好像是默认自带功能
    [ExecuteInEditMode] //允许脚本在编辑器未运行的情况下运行
    [DisallowMultipleComponent] //不允许挂载多个该类或其子类
    [RequireComponent(typeof(Animator))] //添加该组件时自动添加另一组件
    //[SelectionBase]//选择在场景视图中使用此属性的组件对象，即不会误选中子物体
    public class CustomCommand : MonoBehaviour
    {
        //[Header(“Header Name”)] //加粗效果的标题
        //[Space(10)] //表示间隔空间，数字越大，间隔越大
        //[Tooltip(“Tips”)] //显示字段的提示信息
        //[ColorUsage(true)] //显示颜色面板

       
        [Range(0, 100)] //限制数值范围
        public int Range_Int;
       
        [Range(0f, 100)] //限制数值范围
        public float Range_Float;
       
        [Multiline(3)] //字符串多行显示
        public string Multiline_Str;

        [Space(10)] //表示间隔空间，数字越大，间隔越大
        [Header("标题")] //加粗效果的标题
        [Tooltip("Tips")] //显示字段的提示信息
        [TextArea(2, 4)] //文本输入框
        public string TextArea_Str;
        [Space(10),Tooltip("Tips")] //多个属性可用 , 分开写在一起
        public string MultAttr_Str;
        [ColorUsage(true)] //显示颜色面板
        public Color ColorUsage_Color;

        [SerializeField] //序列化字段，主要用于序列化私有字段
        private string SerializeField_PrivateStr;
        
        [NonSerialized] //反序列化一个变量，并且在Inspector上隐藏
        public string NonSerialized_PrivateStr;
        
        [HideInInspector] //public变量在Inspector面板隐藏
        public string HideInInspector_Str;
        
        [FormerlySerializedAs("Value1")] //当变量名发生改变时，可以保存原来Value1的值
        public string FormerlySerializedAs_Str;

        public enum EType
        {
            Type1 = 0,
            Type2,
            Type3
        }
        [HideInInspector]
        public bool PropertyBool;
        [HideInInspector]
        public EType PropertyEnum;
        [HideInInspector]
        public int PropertyValue;

        [ContextMenu("FunctionName")]
        public void FunctionName()
        {
            //给自己的组件右侧下拉菜单选项提供点击方法
        }

        [ContextMenuItem("Handle", "HandleHealth")]
        public float Health;
        private void HandleHealth()
        {
            //给某些属性添加右键菜单属性
        }
    }
}