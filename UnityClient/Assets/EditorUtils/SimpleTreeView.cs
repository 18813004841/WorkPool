using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace EditorUtils
{
    public enum ESimpleTreeViewItemType
    {
        Title,
        Text,
        Button,
        Slider,
        Texture,
        Material,
    }

    class SimpleTreeView : TreeView
    {
        private static int _simpleTreeItemIdPool = 0;
        public static int GetNewSimpleTreeItemId
        {
            get
            {
                _simpleTreeItemIdPool++;
                return _simpleTreeItemIdPool;
            }
        }

        public const int Const_MaxColum = 11;
        public const float kIconWidth = 18f;
        public const float kRowHeights = 20f;
        public const float kToggleWidth = 120f;

        private List<string> Keys = new List<string>();

        public SimpleTreeView(TreeViewState treeViewState)
            : base(treeViewState)
        {
            for (int i = 1; i <= 100; i++)
            {
                Keys.Add(string.Concat("CustomTreeNode", i.ToString()));
            }
            Reload();
            multiColumnHeader = new MultiColumnHeader(CreateStateWith3Cols());
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            //var item = (TreeViewItem<MyTreeElement>)args.item;
            var item = (SimpleTreeViewItem)args.item;
            if (null == item.Options)
            {
                base.RowGUI(args);
                return;
            }

            for (int i = 0; i < args.GetNumVisibleColumns() && i < item.Options.Length; ++i)
            {
                CellGUI(args.GetCellRect(i), (SimpleTreeViewItem)args.item, i, ref args);
            }
        }

        private void CellGUI(Rect cellRect, SimpleTreeViewItem item, int index, ref RowGUIArgs args)
        {
            CenterRectUsingSingleLineHeight(ref cellRect);

            SimpleTreeViewItem.Option option = item.Options[index];

            switch (option.Type)
            {
                case ESimpleTreeViewItemType.Title:
                    args.rowRect = cellRect;
                    base.RowGUI(args);
                    break;
                case ESimpleTreeViewItemType.Text:
                    cellRect.x += item.depth * EditorWindowUtil.Const_TabWidth;
                    GUI.Label(cellRect, ((SimpleTreeViewItem.Option_Tex)option).Text);
                    break;
                case ESimpleTreeViewItemType.Button:
                    GUI.Button(cellRect, ((SimpleTreeViewItem.Option_Button)option).Text);
                    break;
                case ESimpleTreeViewItemType.Slider:
                    EditorGUI.Slider(cellRect, GUIContent.none, ((SimpleTreeViewItem.Option_Slider)option).Value, 0f, 1f);
                    break;
                case ESimpleTreeViewItemType.Texture:
                    break;
                case ESimpleTreeViewItemType.Material:
                    break;
                default:
                    break;
            }
        }

        protected override TreeViewItem BuildRoot()
        {
            // ÿ�ε��� Reload ʱ������ BuildRoot���Ӷ�ȷ��ʹ������
            // ���� TreeViewItem���˴������ǽ������̶���һ�������ʵʾ���У�
            // Ӧ������ģ�ʹ��� TreeView �Լ���ģ�ʹ������

            // �˲���˵�� ID Ӧ����Ψһ�ġ���������
            // ����Ϊ -1�������������ڴ˻����ϵ�����

            var root_View = new SimpleTreeViewItem(SimpleTreeViewItem.Title("����style��")) { id = GetNewSimpleTreeItemId, depth = -1, displayName = "����style��" };
            var root = new SimpleTreeViewItem(SimpleTreeViewItem.Title("����style")) { id = GetNewSimpleTreeItemId, depth = 0, displayName = "����style" };
            for (int i = 0; i < Keys.Count; i++)
            {
                var item = new SimpleTreeViewItem(SimpleTreeViewItem.Title(Keys[i])) { id = GetNewSimpleTreeItemId, depth = 1, displayName = Keys[i] };
                SimpleTreeViewItem.Option[] options = new SimpleTreeViewItem.Option[2];
                options[0] = SimpleTreeViewItem.Tex(Keys[i]);
                if ( i % 2 == 0)
                    options[1] = SimpleTreeViewItem.Slider(0f,1f,0.2f);
                else
                    options[1] = SimpleTreeViewItem.Button(Keys[i]);
                var itemChild = new SimpleTreeViewItem(options)
                { id = GetNewSimpleTreeItemId, depth = 2, displayName = Keys[i] };
                item.AddChild(itemChild);
                root.AddChild(item);
            }
            root_View.AddChild(root);
            // ���ڳ�ʼ��������� TreeViewItem.children �� .parent ��ʵ�÷�����
            //SetupParentsAndChildrenFromDepths(root, allItems);
            SetupDepthsFromParentsAndChildren(root_View);
            //�������ĸ�
            return root_View;
        }

        static MultiColumnHeaderState CreateStateWith3Cols()
        {
            var columns = new MultiColumnHeaderState.Column[11];
            columns[0] = new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent(EditorGUIUtility.FindTexture("FilterByType"), "������tooltips"),
                contextMenuText = "Names",
                width = 150,
                minWidth = 30,
                maxWidth = 200,
                headerTextAlignment = TextAlignment.Center,
                autoResize = false,
                allowToggleVisibility = true,
            };
            for (int i = 1; i < Const_MaxColum; i++)
            {
                columns[i] = new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent(EditorGUIUtility.FindTexture("FilterByType"), "������tooltips"),
                    contextMenuText = string.Concat("P", i.ToString()),
                    width = 150,
                    minWidth = 30,
                    maxWidth = 200,
                    headerTextAlignment = TextAlignment.Center,
                    autoResize = false,
                    allowToggleVisibility = true,
                };
            }
            var state = new MultiColumnHeaderState(columns);
            return state;
        }

    }
}