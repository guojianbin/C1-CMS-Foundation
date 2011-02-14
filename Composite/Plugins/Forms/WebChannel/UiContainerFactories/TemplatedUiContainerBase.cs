﻿using System.Web.UI;
using System.Collections.Generic;
using Composite.C1Console.Forms.DataServices.UiControls;
using Composite.C1Console.Forms.WebChannel;
using Composite.Core.ResourceSystem;
using Composite.Plugins.Forms.WebChannel.UiControlFactories;


namespace Composite.Plugins.Forms.WebChannel.UiContainerFactories
{
    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public abstract class TemplatedUiContainerBase : UserControl
    {
        private IWebUiControl _webUiControl;

        /// <exclude />
        public abstract Control GetFormPlaceHolder();
        
        /// <exclude />
        public abstract Control GetMessagePlaceHolder();

        /// <exclude />
        public abstract void SetContainerTitle(string title);

        /// <exclude />
        public abstract void SetContainerIcon(ResourceHandle icon);


        internal void SetWebUiControlRef(IWebUiControl webUiControl)
        {
            _webUiControl = webUiControl;
        }


        /// <exclude />
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            if (this.IsPostBack == false)
            {
                _webUiControl.InitializeViewState();
            }
            else
            {
                if (_webUiControl is EmbeddedFormUiControl)
                {
                    var container = (_webUiControl as EmbeddedFormUiControl).CompiledUiControl as TemplatedContainerUiControl;
                    if(container != null)
                    {
                        container.InitializeLazyBindedControls();
                    }
                }
            }
        }

        /// <exclude />
        public abstract void ShowFieldMessages(Dictionary<string, string> clientIDPathedMessages);
    }
}
