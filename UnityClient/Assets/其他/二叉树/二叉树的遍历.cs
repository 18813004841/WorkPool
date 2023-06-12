using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace 二叉树
{
	public class 二叉树的遍历
    {

        /// <summary>
        /// 前序遍历
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public List<int> PreorderTraversal(TreeNode root)
        {
            List<int> rt = new List<int>();
            if(null != root) rt.Add(root.val);
            if (null != root.left) rt.AddRange(PreorderTraversal(root.left));
            if (null != root.right) rt.AddRange(PreorderTraversal(root.right));
            return rt;
        }
    }
}