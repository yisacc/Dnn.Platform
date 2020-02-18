﻿// 
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 
using System;
using System.ComponentModel.Composition;

using DotNetNuke.Entities.Icons;
using DotNetNuke.ExtensionPoints;
using DotNetNuke.UI.Modules;

namespace DotNetNuke.Modules.DigitalAssets.Components.ExtensionPoint.ToolBarButton
{
    //TODO Create the Custom IToolBarButtonExtensionPoint Export attribute
    [Export(typeof(IToolBarButtonExtensionPoint))]
    [ExportMetadata("Module", "DigitalAssets")]
    [ExportMetadata("Name", "DigitalAssetsToolBarButton")]
    [ExportMetadata("Group", "Main")]
    [ExportMetadata("Priority", 1)]
    public class CreateFolderToolBarButtonExtensionPoint : IToolBarButtonExtensionPoint
    {
        public string ButtonId
        {
            get { return "DigitalAssetsCreateFolderBtnId"; }
        }

        public string CssClass
        {
            get { return "middleButton split leftAligned folderRequired permission_ADD"; }
        }

        public string Action
        {
            get { return "dnnModule.digitalAssets.createFolder()"; }
        }

        public string AltText
        {
            get { return LocalizationHelper.GetString("CreateFolderToolBarButtonExtensionPoint.AltText"); }
        }

        public bool ShowText
        {
            get { return false; }
        }

        public bool ShowIcon
        {
            get { return true; }
        }

        public string Text
        {
            get { return LocalizationHelper.GetString("CreateFolderToolBarButtonExtensionPoint.Text"); }
        }

        public string Icon
        {
            get { return IconController.IconURL("FolderCreate", "16x16", "Gray"); }
        }

        public int Order
        {
            get { return 7; }
        }

        public bool Enabled
        {
            get { return true; }
        }

        public ModuleInstanceContext ModuleContext { get; set; }
    }
}
