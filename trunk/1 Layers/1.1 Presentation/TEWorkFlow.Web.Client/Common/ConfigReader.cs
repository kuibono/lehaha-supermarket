using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace TEWorkFlow.Web.Client.Common
{
    public class ConfigReader
    {

        /// <summary>
        /// 系统版本号
        /// </summary>
		public static string JTBVersion
        {
			get { return ConfigurationManager.AppSettings["JTBVersion"]; }
        }
        /// <summary>
        /// 显示tiff文件的页面
        /// </summary>
        public static string TiffPage
        {
            get { return ConfigurationManager.AppSettings["Tiffpage"]; }
        }


    }
}