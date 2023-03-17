using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace EditorUtils
{
    public class SimpleTreeViewItem : TreeViewItem
    {
        public class Option
        {
            protected ESimpleTreeViewItemType _type;
            public ESimpleTreeViewItemType Type => _type;
        }

        public class Option_Title: Option
        {
            private string _text;
            public string Text => _text;

            public Option_Title(string text)
            {
                _type = ESimpleTreeViewItemType.Title;
                this._text = text;
            }
        }

        public class Option_Tex : Option
        {
            private string _text;
            public string Text => _text;

            public Option_Tex(string text)
            {
                _type = ESimpleTreeViewItemType.Text;
                this._text = text;
            }
        }

        public class Option_Button : Option
        {
            private string _text;
            public string Text => _text;

            public Option_Button(string text)
            {
                _type = ESimpleTreeViewItemType.Button;
                this._text = text;
            }
        }

        public class Option_Slider : Option
        {
            private float _value;
            public float Value => _value;

            private float _min;
            public float Min => _min;

            private float _max;
            public float Max => _max;

            public Option_Slider(float min, float max, float value = 0)
            {
                _type = ESimpleTreeViewItemType.Slider;
                this._min = min;
                this._max = max;
                this._value = value;
            }
        }

        private Option[] _options;
        public Option[] Options => _options;

        public static Option Title(string text)
        {
            return new Option_Title(text);
        }

        public static Option Tex(string text)
        {
            return new Option_Tex(text);
        }

        public static Option Button(string text)
        {
            return new Option_Button(text);
        }

        public static Option Slider(float min, float max, float value = 0)
        {
            return new Option_Slider(min, max, value);
        }

        public SimpleTreeViewItem(params Option[] options)
        {
            _options = options;
        }
    } 
}