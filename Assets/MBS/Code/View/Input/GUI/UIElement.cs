#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MBS.View.Input.GUI
{
    internal class UIElement
    {
        internal enum ElementType
        {
            Label,
            Toggle,
            Dropdown,
            FloatInput,
            IntegerInput
        }

        public ElementType type;

        
        public KeyCode Key { get; set; }
        public string Label { get; set; }
        public Action KeyAction { get; set; }

        internal UIElement( )
        {
            type = ElementType.Label;
        }
    }

    internal class UIToggleElementData : UIElement
    {
        public string OnText { get; set; }
        public string OffText { get; set; }
        public Func<bool> GetValueRemote { get; set; }
        public Action<bool> OnValueChangeAction { get; set; }


        internal UIToggleElementData( )
        {
            type = ElementType.Toggle;
        }
    }

    internal class UIDropdownElement : UIElement
    {
        public List<string> Choices { get; set; }
        public Func<string> GetValueRemote { get; set; }
        public Action<string> OnValueChangeAction { get; set; }


        internal UIDropdownElement( )
        {
            type = ElementType.Dropdown;
        }
    }

    internal class UIFloatInputData : UIElement
    {
        public Func<float> GetValueRemote { get; set; }
        public Action<float> OnValueChangeAction { get; set; }
        
        internal UIFloatInputData( )
        {
            type = ElementType.FloatInput;
        }
    }
    
    internal class UIIntegerInputData : UIElement
    {
        public Func<int> GetValueRemote { get; set; }
        public Action<int> OnValueChangeAction { get; set; }
        
        internal UIIntegerInputData( )
        {
            type = ElementType.IntegerInput;
        }
    }
}

#endif