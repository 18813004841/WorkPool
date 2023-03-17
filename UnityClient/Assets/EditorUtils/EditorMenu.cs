using UnityEditor;
using UnityEngine;

namespace EditorUtils
{
    public class EditorMenu
    {

        #region 右键菜单

        [MenuItem("Assets/EditorUtil")]
        static void AssetEditorUtil()
        {
            //Assets文件夹扩展
            UnityEngine.Debug.Log("ExtendBtn");
        }

        [MenuItem("GameObject/EditorUtil")]
        static void GameObjectEditorUtil()
        {
            UnityEngine.Debug.Log("ExtendBtn");
        }

        [MenuItem("Assets/Creat/新建cs文件", false, 10000)]
        static void CreateNewCsFile()
        {
            
            //ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance<>)
        }

        #endregion

        #region 顶部菜单

        #region 案例
        /* 快捷键用法 MenuItem("FuncName #F1")
        * 快捷键：
        *  % - Ctrl/CMD; 
        *  # - Shift; 
        *  & - Alt
        *  LEFT/RIGHT/UP/DOWN - Arrow keys
        *  F1 … F2 - F keys
        *  HOME,END,PGUP,PGDN
        */

        [MenuItem("EditorUtils/ExtendBtn")]
        static void TopMenuEditorUtils()
        {
            EditorUtility.DisplayDialog("EditorUtils", "Do ExtendBtn in C# !", "OK", "");
        }
        #endregion

        #region 实用

        [MenuItem("Tools/Window/EditorWindow")]
        public static void OpenEditorWindow()
        {
            EditorWindowUtil.Instance.Open();
        }

        #endregion

        #endregion

    }

}