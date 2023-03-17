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

        public const float Const_TopOffset = 10;   //��������
        public const float Const_ButtonWidth = 100;
        public const int Const_TabWidth = 14;      //�������
        public const int Const_LineHeight = 20;    //�и�
        public const int Const_CutLineHeight = 1;  //�ָ��߸߶�

        void OnEnable()
        {
            Init();
        }

        public void Open()
        {
            
        }

        protected void OnGUI()
        {
            //�������
            GUILayout.Space(Const_TopOffset);
            //����һ������
            GUILayout.BeginArea(new Rect(0, Const_TopOffset, this.position.width, this.position.height));
            //����һ����������
            TabScrollPos = GUILayout.BeginScrollView(TabScrollPos);
            //����һ��������������
            GUILayout.BeginVertical();
            //����һ����ѡ�����б�
            _selectType = (ESelectType)EditorGUILayout.Popup("ѡ��ö��", (int)_selectType, SelectTypeNames,GUILayout.Width(300));
            //����Box �����Ե����ָ���ʹ�ã�
            GUILayout.Box("", GUILayout.Width(this.position.width - 10.0f), GUILayout.Height(Const_CutLineHeight));
            //�ı�
            EditorGUILayout.LabelField("Tab ѡ��", GUILayout.Width(this.position.width - 10.0f));
            //ѡ���ǩ
            _toolBarType = (EToolBarType)GUILayout.Toolbar((int)_toolBarType, ToolBarTypeNames, GUILayout.Width(Const_ButtonWidth * ToolBarTypeNames.Length));

            #region ����һ�� viewtree
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
            //����Ƿ��Ѵ������л���ͼ״̬���ڳ������¼��غ�
            // ��Ȼ���ڵ�״̬��
            if (TV_State == null)
                TV_State = new TreeViewState();
            TV_CustomStyles = new SimpleTreeView(TV_State);
        }
    }
}