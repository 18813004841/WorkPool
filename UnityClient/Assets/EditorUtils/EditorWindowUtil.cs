using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace EditorUtils
{
    public class EditorWindowUtil : EditorWindow
    {
        public enum ESelectType
        {
            Type1,
            Type2,
            Type3
        }

        public enum EToolBarType
        {
            Type1,
            Type2,
            Type3
        }

        public string[] SelectTypeNames = { "Type1", "Type2", "Type3" };
        public string[] ToolBarTypeNames = { "Type1", "Type2", "Type3" };

        private static EditorWindowUtil _instance = null;
        public static EditorWindowUtil Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = EditorWindow.GetWindow<EditorWindowUtil>(false, "EditorWindowUtil", true);
                }
                return _instance;
            }
        }

        private Vector2 TabScrollPos = Vector2.zero;
        ESelectType _selectType = ESelectType.Type1;
        EToolBarType _toolBarType = EToolBarType.Type1;

        private SimpleTreeView TV_CustomStyles;
        private TreeViewState TV_State;

        public const float Const_TopOffset = 10;   //顶部空余
        public const float Const_ButtonWidth = 100;
        public const int Const_TabWidth = 14;      //缩进宽度
        public const int Const_LineHeight = 20;    //行高
        public const int Const_CutLineHeight = 1;  //分割线高度

        void OnEnable()
        {
            Init();
        }

        public void Open()
        {
            
        }

        protected void OnGUI()
        {
            //创建间隔
            GUILayout.Space(Const_TopOffset);
            //创建一个区域
            GUILayout.BeginArea(new Rect(0, Const_TopOffset, this.position.width, this.position.height));
            //创建一个滚动区域
            TabScrollPos = GUILayout.BeginScrollView(TabScrollPos);
            //创建一个纵向排列区域
            GUILayout.BeginVertical();
            //创建一个可选参数列表
            _selectType = (ESelectType)EditorGUILayout.Popup("选择枚举", (int)_selectType, SelectTypeNames,GUILayout.Width(300));
            //创建Box （可以当作分割线使用）
            GUILayout.Box("", GUILayout.Width(this.position.width - 10.0f), GUILayout.Height(Const_CutLineHeight));
            //文本
            EditorGUILayout.LabelField("Tab 选择：", GUILayout.Width(this.position.width - 10.0f));
            //选择标签
            _toolBarType = (EToolBarType)GUILayout.Toolbar((int)_toolBarType, ToolBarTypeNames, GUILayout.Width(Const_ButtonWidth * ToolBarTypeNames.Length));

            #region 创建一棵 viewtree
            float y = EditorGUILayout.BeginVertical().y;
            TV_CustomStyles.OnGUI(new Rect(0, y, position.width, int.MaxValue));
            EditorGUILayout.EndVertical();
            GUILayout.Space(TV_CustomStyles.totalHeight);
            #endregion

            GUILayout.Box("", GUILayout.Width(this.position.width - 10.0f), GUILayout.Height(Const_CutLineHeight));
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        private void Init()
        {
            //检查是否已存在序列化视图状态（在程序集重新加载后
            // 仍然存在的状态）
            if (TV_State == null)
                TV_State = new TreeViewState();
            TV_CustomStyles = new SimpleTreeView(TV_State);
        }
    }
}