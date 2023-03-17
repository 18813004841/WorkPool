using UnityEditor;
using UnityEngine;

namespace EditorUtils
{
    /// <summary>
    /// ���������չ
    /// </summary>
    public class EditorInspectorPanel
    {
        [MenuItem("CONTEXT/Transform/CustomFunc")]
        private static void CustomFunc_Transform()
        {
            //���������Ҽ��������
            Debug.Log("Run CustomFunc");
        }

        [MenuItem("CONTEXT/CustomCommand/CustomFunc")]
        static void Init(MenuCommand cmd)
        {
            //���Զ����������Ҽ��������
            CustomCommand health = cmd.context as CustomCommand;
            Debug.Log("Run CustomFunc");
        }
    }

    [CustomEditor(typeof(CustomCommand), true)] //Ҫ�Զ���༭����Ҫ���������
    [CustomPropertyDrawer(typeof(CustomCommand), true)] //���ڻ����Զ���PropertyDrawer������
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
            //Ѱ�Ҷ�Ӧ������
            m_PropertyBool = @object.FindProperty("PropertyBool");
            m_PropertyEnum = @object.FindProperty("PropertyEnum");
            m_PropertyValue = @object.FindProperty("PropertyValue");
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
           
            _foldoutExtendProperty = EditorGUILayout.Foldout(_foldoutExtendProperty, "��չ����");
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
                //��ʼ����������
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