using UnityEditor;
using UnityEngine;

namespace EditorUtils
{
    public class EditorMenu
    {

        #region �Ҽ��˵�

        [MenuItem("Assets/EditorUtil")]
        static void AssetEditorUtil()
        {
            //Assets�ļ�����չ
            UnityEngine.Debug.Log("ExtendBtn");
        }

        [MenuItem("GameObject/EditorUtil")]
        static void GameObjectEditorUtil()
        {
            UnityEngine.Debug.Log("ExtendBtn");
        }

        [MenuItem("Assets/Creat/�½�cs�ļ�", false, 10000)]
        static void CreateNewCsFile()
        {
            
            //ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance<>)
        }

        #endregion

        #region �����˵�

        #region ����
        /* ��ݼ��÷� MenuItem("FuncName #F1")
        * ��ݼ���
        *  % - Ctrl/CMD; 
        *  # - Shift; 
        *  & - Alt
        *  LEFT/RIGHT/UP/DOWN - Arrow keys
        *  F1 �� F2 - F keys
        *  HOME,END,PGUP,PGDN
        */

        [MenuItem("EditorUtils/ExtendBtn")]
        static void TopMenuEditorUtils()
        {
            EditorUtility.DisplayDialog("EditorUtils", "Do ExtendBtn in C# !", "OK", "");
        }
        #endregion

        #region ʵ��

        [MenuItem("Tools/Window/EditorWindow")]
        public static void OpenEditorWindow()
        {
            EditorWindowUtil.Instance.Open();
        }

        #endregion

        #endregion

    }

}