﻿// 
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 
#region Usings

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Client;
using DotNetNuke.Web.Client.ClientResourceManagement;

#endregion

namespace DotNetNuke.Web.UI.WebControls
{
    public class DnnFormPasswordItem : DnnFormItemBase
    {
        private TextBox _password;

        public string TextBoxCssClass
        {
            get
            {
                return ViewState.GetValue("TextBoxCssClass", string.Empty);
            }
            set
            {
                ViewState.SetValue("TextBoxCssClass", value, string.Empty);
            }
        }

        public string ContainerCssClass
        {
            get
            {
                return ViewState.GetValue("ContainerCssClass", string.Empty);
            }
            set
            {
                ViewState.SetValue("ContainerCssClass", value, string.Empty);
            }
        }

        private void TextChanged(object sender, EventArgs e)
        {
            UpdateDataSource(Value, _password.Text, DataField);
        }

        /// <summary>
        /// Use container to add custom control hierarchy to
        /// </summary>
        /// <param name="container"></param>
        /// <returns>An "input" control that can be used for attaching validators</returns>
        protected override WebControl CreateControlInternal(Control container)
        {
            _password = new TextBox()
            {
                ID = ID + "_TextBox",
                TextMode = TextBoxMode.Password,
                CssClass = TextBoxCssClass,
                MaxLength = 39, //ensure password cannot be cut if too long
                Text = Convert.ToString(Value) // Load from ControlState
            };
            _password.Attributes.Add("autocomplete", "off");
            _password.Attributes.Add("aria-label", DataField);
            _password.TextChanged += TextChanged;

            var passwordContainer = new Panel() { ID = "passwordContainer", CssClass = ContainerCssClass };

            // add control hierarchy to the container
            container.Controls.Add(passwordContainer);

            passwordContainer.Controls.Add(_password);

            // return input control that can be used for validation
            return _password;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ClientResourceManager.RegisterScript(Page, "~/Resources/Shared/scripts/dnn.jquery.extensions.js");
            ClientResourceManager.RegisterScript(Page, "~/Resources/Shared/scripts/dnn.jquery.tooltip.js");
            ClientResourceManager.RegisterScript(Page, "~/Resources/Shared/scripts/dnn.PasswordStrength.js");

			ClientResourceManager.RegisterStyleSheet(Page, "~/Resources/Shared/stylesheets/dnn.PasswordStrength.css", FileOrder.Css.ResourceCss);

            JavaScript.RequestRegistration(CommonJs.DnnPlugins);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            var options = new DnnPaswordStrengthOptions();
            var optionsAsJsonString = Json.Serialize(options);
            var script = string.Format("dnn.initializePasswordStrength('.{0}', {1});{2}",
                TextBoxCssClass, optionsAsJsonString, Environment.NewLine);

            if (ScriptManager.GetCurrent(Page) != null)
            {
                // respect MS AJAX
                ScriptManager.RegisterStartupScript(Page, GetType(), "PasswordStrength", script, true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "PasswordStrength", script, true);
            }

        }

    }

}
