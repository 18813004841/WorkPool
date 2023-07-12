using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ClassAndStruct
{
    public class Class:MonoBehaviour
    {
        private void Awake()
        {
            Main();
        }

        public void Main()
        {
            var child = new ChildClass_1();
            child.Log();
        }
    }

    public class ClassBase
    {
        private static ClassBase _instance;

        private static int id = 0;

        public static ClassBase Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new ClassBase();
                }
                return _instance;
            }
        }

        public void Log()
        {
            Debug.Log(id.ToString());
        }

        protected ClassBase()
        {
            Debug.Log("ClassBase");
            id += 1;
        }
    }

    public class ChildClass_1 : ClassBase
    {
        public ChildClass_1()
        {
            Debug.Log("ChildClass_1");
        }
    }
}
