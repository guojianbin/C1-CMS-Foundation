﻿using System;
using System.Collections.Generic;


namespace Composite.Data
{
    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
    public sealed class DataScopeAttribute : Attribute
    {


        /// <exclude />
        public DataScopeAttribute(string dataScope)
        {
            this.Identifier = Composite.Data.DataScopeIdentifier.Deserialize(dataScope);
        }


        /// <exclude />
        public DataScopeIdentifier Identifier
        {
            get;
            private set;
        }
    }
}
