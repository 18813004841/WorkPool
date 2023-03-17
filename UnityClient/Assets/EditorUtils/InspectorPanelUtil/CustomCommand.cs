using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace EditorUtils
{

    [AddComponentMenu("Custom/CustomCommand")] //�����ڲ˵���Component����������ť
    //[CanEditMultipleObjects] //����ѡ�������иýű��Ķ���ʱ��ͳһ�޸�ֵ�����������Ĭ���Դ�����
    [ExecuteInEditMode] //����ű��ڱ༭��δ���е����������
    [DisallowMultipleComponent] //��������ض�������������
    [RequireComponent(typeof(Animator))] //��Ӹ����ʱ�Զ������һ���
    //[SelectionBase]//ѡ���ڳ�����ͼ��ʹ�ô����Ե�������󣬼�������ѡ��������
    public class CustomCommand : MonoBehaviour
    {
        //[Header(��Header Name��)] //�Ӵ�Ч���ı���
        //[Space(10)] //��ʾ����ռ䣬����Խ�󣬼��Խ��
        //[Tooltip(��Tips��)] //��ʾ�ֶε���ʾ��Ϣ
        //[ColorUsage(true)] //��ʾ��ɫ���

       
        [Range(0, 100)] //������ֵ��Χ
        public int Range_Int;
       
        [Range(0f, 100)] //������ֵ��Χ
        public float Range_Float;
       
        [Multiline(3)] //�ַ���������ʾ
        public string Multiline_Str;

        [Space(10)] //��ʾ����ռ䣬����Խ�󣬼��Խ��
        [Header("����")] //�Ӵ�Ч���ı���
        [Tooltip("Tips")] //��ʾ�ֶε���ʾ��Ϣ
        [TextArea(2, 4)] //�ı������
        public string TextArea_Str;
        [Space(10),Tooltip("Tips")] //������Կ��� , �ֿ�д��һ��
        public string MultAttr_Str;
        [ColorUsage(true)] //��ʾ��ɫ���
        public Color ColorUsage_Color;

        [SerializeField] //���л��ֶΣ���Ҫ�������л�˽���ֶ�
        private string SerializeField_PrivateStr;
        
        [NonSerialized] //�����л�һ��������������Inspector������
        public string NonSerialized_PrivateStr;
        
        [HideInInspector] //public������Inspector�������
        public string HideInInspector_Str;
        
        [FormerlySerializedAs("Value1")] //�������������ı�ʱ�����Ա���ԭ��Value1��ֵ
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
            //���Լ�������Ҳ������˵�ѡ���ṩ�������
        }

        [ContextMenuItem("Handle", "HandleHealth")]
        public float Health;
        private void HandleHealth()
        {
            //��ĳЩ��������Ҽ��˵�����
        }
    }
}