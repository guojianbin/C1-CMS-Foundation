﻿using System;


namespace Composite.Data.DynamicTypes
{
    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    [Serializable()]
    public class DataFieldFormRenderingProfile
    {
        /// <exclude />
        public virtual string Label { get; set; }

        /// <exclude />
        public virtual string HelpText { get; set; }

        /// <exclude />
        public virtual string WidgetFunctionMarkup { get; set; }
    }



    [Serializable()]
    internal class LazyDataFieldFormRenderingProfile : DataFieldFormRenderingProfile
    {
        [NonSerialized]
        private string _widgetFunctionMarkup = null;

        [NonSerialized]
        private Func<string> _widgetFunctionMarkupFunc;


        public Func<string> WidgetFunctionMarkupFunc { get { return _widgetFunctionMarkupFunc; } set { _widgetFunctionMarkupFunc = value; } }


        public override string WidgetFunctionMarkup
        {
            get
            {
                if (_widgetFunctionMarkup == null)
                {
                    _widgetFunctionMarkup = WidgetFunctionMarkupFunc();
                }

                return _widgetFunctionMarkup;
            }
            set
            {
                _widgetFunctionMarkup = value;
            }
        }
    }
}
