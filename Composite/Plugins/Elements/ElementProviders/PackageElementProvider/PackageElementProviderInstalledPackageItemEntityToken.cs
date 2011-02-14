﻿using System;
using System.Collections.Generic;
using System.Text;
using Composite.C1Console.Security;
using Composite.Core.Serialization;


namespace Composite.Plugins.Elements.ElementProviders.PackageElementProvider
{
    internal sealed class AddOnElementProviderInstalledAddOnItemEntityTokenAncestorProvider : ISecurityAncestorProvider
    {
        public IEnumerable<EntityToken> GetParents(EntityToken entityToken)
        {
            if (entityToken == null) throw new ArgumentNullException("entityToken");

            PackageElementProviderInstalledPackageItemEntityToken castedEntityToken = (PackageElementProviderInstalledPackageItemEntityToken)entityToken;

            yield return new PackageElementProviderInstalledPackageGroupFolderEntityToken(castedEntityToken.GroupName);
        }
    }




    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    [SecurityAncestorProvider(typeof(AddOnElementProviderInstalledAddOnItemEntityTokenAncestorProvider))]
    public sealed class PackageElementProviderInstalledPackageItemEntityToken : EntityToken
    {
        /// <exclude />
        public PackageElementProviderInstalledPackageItemEntityToken(Guid packageId, string groupName, bool isLocalInstalled, bool canBeUninstalled)
        {
            this.AddOnId = packageId;
            this.GroupName = groupName;
            this.IsLocalInstalled = isLocalInstalled;
            this.CanBeUninstalled = canBeUninstalled;
        }


        /// <exclude />
        public Guid AddOnId { get; private set; }

        /// <exclude />
        public string GroupName { get; private set; }

        /// <exclude />
        public bool IsLocalInstalled { get; private set; }

        /// <exclude />
        public bool CanBeUninstalled { get; private set; }


        /// <exclude />
        public override string Type
        {
            get { return ""; }
        }


        /// <exclude />
        public override string Source
        {
            get { return ""; }
        }


        /// <exclude />
        public override string Id
        {
            get { return this.AddOnId.ToString(); }
        }


        /// <exclude />
        public override string Serialize()
        {
            StringBuilder sb = new StringBuilder();

            StringConversionServices.SerializeKeyValuePair(sb, "_GroupName_", this.GroupName);
            StringConversionServices.SerializeKeyValuePair(sb, "_IsLocalInstalled_", this.CanBeUninstalled);
            StringConversionServices.SerializeKeyValuePair(sb, "_CanBeUninstalled_", this.CanBeUninstalled);

            DoSerialize(sb);

            return sb.ToString();
        }


        /// <exclude />
        public static EntityToken Deserialize(string serializedEntityToken)
        {
            string type, source, id;
            Dictionary<string, string> dic;

            DoDeserialize(serializedEntityToken, out type, out source, out id, out dic);

            if ((dic.ContainsKey("_GroupName_") == false) || (dic.ContainsKey("_IsLocalInstalled_") == false) || (dic.ContainsKey("_CanBeUninstalled_") == false)) throw new ArgumentException("serializedEntityToken is of wrong format");

            string groupName = StringConversionServices.DeserializeValueString(dic["_GroupName_"]);
            bool isLocalInstalled = StringConversionServices.DeserializeValueBool(dic["_IsLocalInstalled_"]);
            bool canBeUninstalled = StringConversionServices.DeserializeValueBool(dic["_CanBeUninstalled_"]);

            return new PackageElementProviderInstalledPackageItemEntityToken(new Guid(id), groupName, isLocalInstalled, canBeUninstalled);
        }


        /// <exclude />
        public override int GetHashCode()
        {
            return base.GetHashCode() ^ this.GroupName.GetHashCode() ^ this.CanBeUninstalled.GetHashCode();
        }
    }
}
