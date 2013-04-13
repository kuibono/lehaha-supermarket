using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TEWorkFlow.Web.Client.Common
{
    public class LocalizedDisplayName : DisplayNameAttribute
    {

        private string _defaultName = "";

        public Type ResourceType
        {
            get;
            set;
        }

        public string ResourceName
        {
            get;
            set;
        }

        public LocalizedDisplayName(string defaultName)
        {
            _defaultName = defaultName;
        }

        public override string DisplayName
        {
            get
            {
                PropertyInfo p = ResourceType.GetProperty(ResourceName);
                if (p != null)
                    return p.GetValue(null, null).ToString();
                else
                    return _defaultName;
            }
        }

    }
}