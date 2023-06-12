using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AllAround_ScrollView), true)]
public class Editor_AllAround_ScrollView : Editor
{
    private SerializedObject @object;

    //private SerializedProperty m_PropertyBool;
    //private SerializedProperty m_PropertyEnum;
    //private SerializedProperty m_PropertyValue;

    private AllAround_ScrollView m_Script;
    private bool _foldoutExtendProperty = false;

    private void OnEnable()
    {
        @object = new SerializedObject(target);
        m_Script = @object.targetObject as AllAround_ScrollView;
        //Ѱ�Ҷ�Ӧ������
        //m_PropertyBool = @object.FindProperty("PropertyBool");
        //m_PropertyEnum = @object.FindProperty("PropertyEnum");
        //m_PropertyValue = @object.FindProperty("PropertyValue");
    }

    public override void OnInspectorGUI()
    {
        GUIStyle MiddleCenterStyle = GUI.skin.label;//��unityĬ�ϵ���ʽ�����޸ģ����ʹ��new GUIStyle()�Ļ������Ƴ�����Labelֻ�����Լ����õ���ʽ
        MiddleCenterStyle.alignment = TextAnchor.MiddleCenter;//
        MiddleCenterStyle.normal.background = Texture2D.grayTexture;
        GUILayout.Label("�ű�������", MiddleCenterStyle);
        MiddleCenterStyle.normal.background = null;//������������ܻᵼ��unity�����ط����Ƴ���
                                                   //GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        base.OnInspectorGUI();

        if (m_Script.Scroll.horizontal!= m_Script.UseHorizontal)
        {
            m_Script.Scroll.horizontal = m_Script.UseHorizontal;
            //m_Script.Scroll.viewport.sizeDelta = new Vector2(14, m_Script.UseHorizontal ? -20 : 0);
        }
        if (m_Script.HorizontalBar.gameObject.activeSelf != m_Script.UseHorizontal)
        {
            m_Script.HorizontalBar.gameObject.SetActive(m_Script.UseHorizontal);
            m_Script.Scroll.viewport.sizeDelta = new Vector2(-20, m_Script.UseHorizontal ? -20 : 0);
        }

        @object.ApplyModifiedProperties();
    }
}
