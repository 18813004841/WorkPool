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
        //寻找对应的属性
        //m_PropertyBool = @object.FindProperty("PropertyBool");
        //m_PropertyEnum = @object.FindProperty("PropertyEnum");
        //m_PropertyValue = @object.FindProperty("PropertyValue");
    }

    public override void OnInspectorGUI()
    {
        GUIStyle MiddleCenterStyle = GUI.skin.label;//在unity默认的样式上面修改，如果使用new GUIStyle()的话，绘制出来的Label只有你自己设置的样式
        MiddleCenterStyle.alignment = TextAnchor.MiddleCenter;//
        MiddleCenterStyle.normal.background = Texture2D.grayTexture;
        GUILayout.Label("脚本内属性", MiddleCenterStyle);
        MiddleCenterStyle.normal.background = null;//不调用这个可能会导致unity其他地方绘制出错
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
