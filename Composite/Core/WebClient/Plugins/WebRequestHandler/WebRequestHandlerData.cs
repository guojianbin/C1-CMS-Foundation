using System.Configuration;
using Composite.Core.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;


namespace Composite.Core.WebClient.Plugins.WebRequestHandler
{
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    [ConfigurationElementType(typeof(NonConfigurableWebRequestHandler))]
    public class WebRequestHandlerData : NameTypeManagerTypeConfigurationElement
    {
        private const string _templateControlFilePropertyName = "templateControlFile";
        /// <exclude />
        [ConfigurationProperty(_templateControlFilePropertyName, IsRequired = true)]
        public string TemplateControlFile
        {
            get { return (string)base[_templateControlFilePropertyName]; }
            set { base[_templateControlFilePropertyName] = value; }
        }



        private const string _templateControlPlaceholderIdPropertyName = "templateControlPlaceholderId";
        /// <exclude />
        [ConfigurationProperty(_templateControlPlaceholderIdPropertyName, IsRequired = true)]
        public string TemplateControlPlaceholderId
        {
            get { return (string)base[_templateControlPlaceholderIdPropertyName]; }
            set { base[_templateControlPlaceholderIdPropertyName] = value; }
        }
    }
}
