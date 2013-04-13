﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;
using System.Reflection;
using System.Web.UI.WebControls;

namespace NSH.VSTO
{
    public static class Extend
    {

        #region 取得Request的string值，任何错误均返回""，Form优先于QueryString被取出
        /// <summary>
        /// 取得Request的string值，任何错误均返回""，Form优先于QueryString被取出
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns>键值</returns>
        public static string RequestString(object key)
        {
            return RequestString(key, "");
        }
        /// <summary>
        /// 取得Request的string值，Form优先于QueryString被取出
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <param name="defaultValue">如果获取不到，返回的默认值</param>
        /// <returns>出a现错误返回默认值</returns>
        public static string RequestString(object key, string defaultValue)
        {
            if (HttpContext.Current.Request.Form[key.ToString()] != null)
            {
                return System.Web.HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.Form[key.ToString()].ToString()).ToString();
            }
            if (HttpContext.Current.Request.QueryString[key.ToString()] != null)
            {
                return System.Web.HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.QueryString[key.ToString()].ToString());
            }
            return defaultValue;
        }

        #endregion 通过Request对象取得的值


        public static StringBuilder TrimEnd(this StringBuilder self, char c)
        {
            string s = self.ToString();
            s = s.TrimEnd(c);
            StringBuilder sb = new StringBuilder();
            sb.Append(s);
            return sb;
        }

        public static string ListToJsonStr(this IEnumerable<object> lst)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(RequestString("jsoncallback") + "({");
            sb.Append(string.Format("\"total\":{0},",lst.Count()));

            sb.Append("\"rows\":[");
            foreach (object o in lst)
            {
                ArrayList list = new ArrayList();

                PropertyInfo[] fieldinfo = o.GetType().GetProperties();

                foreach (PropertyInfo info in fieldinfo)
                {
                    ListItem listitem = new ListItem(info.Name, info.GetValue(o, null).ToString());
                    list.Add(listitem);
                }
                sb.Append("{");

                for (int i = 0; i < list.Count; i++)
                {

                    ListItem li = (ListItem)list[i];

                    sb.Append("\"" + li.Text.Replace("\"", "\\\"").Replace("'", "\\'") + "\":");
                    sb.Append("\"" + li.Value.Replace("\"", "\\\"").Replace("'", "\\'") + "\"");

                    if (i != list.Count - 1)
                    {
                        sb.Append(",");
                    }

                }
                sb.Append("},");

            }

            sb = TrimEnd(sb, ',');

            sb.Append("])}");
            return sb.ToString();
        }
    }
}
