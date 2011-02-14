﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Composite.Data.DynamicTypes
{
    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public sealed class DataFieldDescriptorCollection : IEnumerable<DataFieldDescriptor>
    {
        private DataTypeDescriptor _parent;
        private List<DataFieldDescriptor> _descriptors = new List<DataFieldDescriptor>();


        internal DataFieldDescriptorCollection(DataTypeDescriptor parent) 
        {
            _parent = parent;
        }


        /// <exclude />
        public int Count
        {
            get { return _descriptors.Count; }
        }


        /// <exclude />
        public void Add(DataFieldDescriptor descriptor)
        {
            if (descriptor == null) throw new ArgumentNullException("descriptor");
            if (_descriptors.Contains(descriptor) == true) throw new ArgumentException("The specied DataFieldDescriptor has already been added. Developers should ensure that the Immutable Field Id is unique on all fields.");
            if (this[descriptor.Name] != null) throw new ArgumentException("The specified Name is in use by another DataFieldDescriptor");
            if (this[descriptor.Id] != null) throw new ArgumentException("The specified Id is in use by another DataFieldDescriptor");

            _descriptors.Add(descriptor);
        }


        /// <exclude />
        public void Insert(int index, DataFieldDescriptor descriptor)
        {
            if (descriptor == null) throw new ArgumentNullException("descriptor");
            if (_descriptors.Contains(descriptor) == true) throw new ArgumentException("The specied DataFieldDescriptor has already been added");
            if (this[descriptor.Name] != null) throw new ArgumentException("The specified Name is in use by another DataFieldDescriptor");
            if (this[descriptor.Id] != null) throw new ArgumentException("The specified Id is in use by another DataFieldDescriptor");

            _descriptors.Insert(index, descriptor);
        }


        /// <exclude />
        public void Remove(DataFieldDescriptor descriptor)
        {
            if (descriptor == null) throw new ArgumentNullException("descriptor");
            if (_descriptors.Contains(descriptor) == false) throw new ArgumentException("The specied DataFieldDescriptor was not found");
            if (_parent.KeyPropertyNames.Contains(descriptor.Name)) throw new ArgumentException("The DataFieldDescriptor can not be removed while it is a member of the key field list.");
            if (_parent.StoreSortOrderFieldNames.Contains(descriptor.Name)) throw new ArgumentException("The DataFieldDescriptor can not be removed while it is a member of the physical sort order field list.");

            _descriptors.Remove(descriptor);
        }


        /// <exclude />
        public bool Contains(DataFieldDescriptor descriptor)
        {
            if (descriptor == null) throw new ArgumentNullException("descriptor");

            return _descriptors.Contains(descriptor);
        }


        /// <exclude />
        public DataFieldDescriptor this[string name]
        {
            get { return _descriptors.SingleOrDefault(d => d.Name == name); }
        }


        /// <exclude />
        public DataFieldDescriptor this[Guid id]
        {
            get { return _descriptors.SingleOrDefault(d => d.Id == id); }
        }


        /// <exclude />
        public IEnumerator<DataFieldDescriptor> GetEnumerator()
        {
            return _descriptors.OrderBy(f=>f.Position).GetEnumerator();
        }


        /// <exclude />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _descriptors.OrderBy(f => f.Position).GetEnumerator();
        }
    }
}
