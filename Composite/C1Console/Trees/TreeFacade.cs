﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Composite.C1Console.Elements.Plugins.ElementAttachingProvider;
using Composite.C1Console.Events;
using Composite.C1Console.Security;



namespace Composite.C1Console.Trees
{
    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public static class TreeFacade
    {
        private static ITreeFacade _implementation = new TreeFacadeImpl();
        private static object _lock = new object();
        private static bool _initialized = false;


        static TreeFacade()
        {
            GlobalEventSystemFacade.SubscribeToPostFlushEvent(OnPostFlushEvent);
        }



        /// <exclude />
        public static void Initialize()
        {
            lock (_lock)
            {
                if (_initialized == false)
                {
                    _implementation.Initialize();
                    _initialized = true;
                }
            }
        }



        /// <summary>
        /// Returns a tree given the id of the tree or null if no tree exist with the given id
        /// </summary>
        /// <param name="treeId"></param>
        /// <returns></returns>
        public static Tree GetTree(string treeId)
        {
            return _implementation.GetTree(treeId);
        }



        /// <exclude />
        public static IEnumerable<Tree> AllTrees
        {
            get
            {
                return _implementation.AllTrees;
            }
        }



        /// <exclude />
        public static bool HasAttachmentPoints(EntityToken parentEntityToken)
        {
            return _implementation.HasAttachmentPoints(parentEntityToken);
        }



        /// <exclude />
        public static bool HasPossibleAttachmentPoints(EntityToken parentEntityToken)
        {
            return _implementation.HasPossibleAttachmentPoints(parentEntityToken);
        }



        /// <exclude />
        public static IEnumerable<Tree> GetTreesByEntityToken(EntityToken parentEntityToken)
        {
            return _implementation.GetTreesByEntityToken(parentEntityToken);
        }



        /// <summary>
        /// Adds a attachment point that is persisted by the system and is loaded on every restart
        /// </summary>
        /// <param name="treeId"></param>
        /// <param name="interfaceType"></param>
        /// <param name="keyValue"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool AddPersistedAttachmentPoint(string treeId, Type interfaceType, object keyValue, ElementAttachingProviderPosition position = ElementAttachingProviderPosition.Top)
        {
            return _implementation.AddPersistedAttachmentPoint(treeId, interfaceType, keyValue, position);
        }



        /// <exclude />
        public static bool RemovePersistedAttachmentPoint(string treeId, Type interfaceType, object keyValue)
        {
            return _implementation.RemovePersistedAttachmentPoint(treeId, interfaceType, keyValue);
        }



        /// <summary>
        /// This will add a attachment point until the system flushes.
        /// This can be used by element provider implementors to attach trees to their exising trees.
        /// </summary>
        /// <param name="treeId"></param>
        /// <param name="entityToken"></param>
        /// <param name="position"></param>
        public static bool AddCustomAttachmentPoint(string treeId, EntityToken entityToken, ElementAttachingProviderPosition position = ElementAttachingProviderPosition.Top)
        {
            return _implementation.AddCustomAttachmentPoint(treeId, entityToken, position);
        }



        /// <exclude />
        public static Tree LoadTreeFromDom(string treeId, XDocument document)
        {
            return _implementation.LoadTreeFromDom(treeId, document);
        }



        private static void OnPostFlushEvent(PostFlushEventArgs args)
        {
            _implementation.OnFlush();
        }
    }
}
