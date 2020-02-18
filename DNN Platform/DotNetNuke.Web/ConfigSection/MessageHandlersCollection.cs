﻿// 
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 
using System.Configuration;

namespace DotNetNuke.Web.ConfigSection
{
    public class MessageHandlersCollection : ConfigurationElementCollection
    {
        public MessageHandlerEntry this[int index]
        {
            get
            {
                return BaseGet(index) as MessageHandlerEntry;
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new MessageHandlerEntry();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as MessageHandlerEntry ?? new MessageHandlerEntry()).Name;
        }
    }
}
