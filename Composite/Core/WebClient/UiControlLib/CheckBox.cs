﻿using System;
using System.Web.UI;
using Composite.Core.ResourceSystem;
using Composite.Core.WebClient.UiControlLib.Foundation;



namespace Composite.Core.WebClient.UiControlLib
{
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public class CheckBox : System.Web.UI.WebControls.CheckBox
    {
        /// <exclude />
        public string ItemLabel { get; set; }



        /// <exclude />
        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("ui:checkbox");

            writer.WriteAttribute("label", StringResourceSystemFacade.ParseString((this.ItemLabel ?? "")));

            if (string.IsNullOrEmpty(this.ToolTip) == false)
            {
                writer.WriteAttribute("title", StringResourceSystemFacade.ParseString((this.ToolTip ?? "")));
            }

            writer.WriteAttribute("name", this.UniqueID);

            if (this.AutoPostBack == true)
            {
                throw new NotImplementedException("The CheckBox AutoPostBack feature is volatile. Event is not raised in certain circumstances");
            }

            writer.WriteAttribute("ischecked", this.Checked.ToString().ToLower());

            this.WriteClientAttributes(writer);

            writer.Write(HtmlTextWriter.SelfClosingTagEnd);
        }
    }
}