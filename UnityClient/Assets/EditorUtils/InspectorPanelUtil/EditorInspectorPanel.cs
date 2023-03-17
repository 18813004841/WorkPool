using UnityEditor;
using UnityEngine;

namespace EditorUtils
{
    /// <summary>
    /// 属性面板扩展
    /// </summary>
    public class EditorInspectorPanel
    {
        [MenuItem("CONTEXT/Transform/CustomFunc")]
        private static void CustomFunc_Transform()
        {
            //给组件添加右键点击方法
            Debug.Log("Run CustomFunc");
        }

        [MenuItem("CONTEXT/CustomCommand/CustomFunc")]
        static void Init(MenuCommand cmd)
        {
            //给自定义组件添加右键点击方法
            CustomCommand health = cmd.context as CustomCommand;
            Debug.Log("Run CustomFunc");
        }
    }

    [CustomEditor(typeof(CustomCommand), true)] //要自定义编辑器就要加这个特性
    [CustomPropertyDrawer(typeof(CustomCommand), true)] //用于绘制自定义PropertyDrawer的特性
    public class EditorCustomCommand : Editor
    {
        private SerializedObject @object;

        private SerializedProperty m_PropertyBool;
        private SerializedProperty m_PropertyEnum;
        private SerializedProperty m_PropertyValue;

        private bool _foldoutExtendProperty = false;

        private void OnEnable()
        {
            @object = new SerializedObject(target);
            //寻找对应的属性
            m_PropertyBool = @object.FindProperty("PropertyBool");
            m_PropertyEnum = @object.FindProperty("PropertyEnum");
            m_PropertyValue = @object.FindProperty("PropertyValue");
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
           
            _foldoutExtendProperty = EditorGUILayout.Foldout(_foldoutExtendProperty, "扩展属性");
            if (_foldoutExtendProperty)
            {
                @object.Update();
                SerializedProperty property = @object.GetIterator();
                while (property.NextVisible(true))
                {
                    using (new EditorGUI.DisabledScope("m_Script" == property.propertyPath))
                    {
                        EditorGUILayout.PropertyField(property, true);
                        break;
                    }
                }
                //开始设计属性面板
                EditorGUILayout.PropertyField(m_PropertyBool);
                if (m_PropertyBool.boolValue)
                {
                    EditorGUILayout.PropertyField(m_PropertyEnum);
                    if (m_PropertyEnum.enumValueIndex == 0)
                    {
                        EditorGUILayout.PropertyField(m_PropertyValue);
                    }
                }
                @object.ApplyModifiedProperties();
            }
        }
    }
}