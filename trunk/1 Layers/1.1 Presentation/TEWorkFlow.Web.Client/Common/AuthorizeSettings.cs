using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Configuration;

namespace TEWorkFlow.Web.Client.Common
{
    public class AuthorizeSettings
    {
        private static NameValueCollection settings = ConfigurationManager.GetSection("authorizeSettings") as NameValueCollection;

        /// <summary>
        /// 用户验证Session名称(以作废！）
        /// </summary>
        public static string SessionName
        {
            get { return settings["SessionName"]; }
        }
        /// <summary>
        /// 用户名session节点（session）
        /// </summary>
        public static string SessionUserName
        {
            get { return settings["SessionUserName"]; }
        }

        public static string SessionUserType
        {
            get { return settings["SessionUserType"]; }
        }

        /// <summary>
        /// 用户idsession节点（session）
        /// </summary>
        public static string SessionUserID
        {
            get { return settings["SessionUserID"]; }
        }
        /// <summary>
        /// 用户组session节点（session）
        /// </summary>
        public static string SessionUserGroup
        {
            get { return settings["SessionUserGroup"]; }
        }
        /// <summary>
        /// 用户角色session节点（session）
        /// </summary>
        public static string SessionUserRole
        {
            get { return settings["SessionUserRole"]; }
        }
        /// <summary>
        /// 用户组code
        /// </summary>
        public static string SessionUserGroupCode
        {
            get { return settings["SessionUserGroupCode"]; }
        }
        /// <summary>
        /// 用户角色code
        /// </summary>
        public static string SessionUserRoleCode
        {
            get { return settings["SessionUserRoleCode"]; }
        }

        /// <summary>
        /// cookie节点名称（cookie）
        /// </summary>
        public static string CookieScope
        {
            get { return settings["CookieScope"]; }
        }
        /// <summary>
        /// 用户名节点名称（cookie）
        /// </summary>
        public static string CookieName
        {
            get { return settings["CookieName"]; }
        }
        /// <summary>
        /// 用户密码节点名称（cookie）
        /// </summary>
        public static string CookiePassword
        {
            get { return settings["CookiePassword"]; }
        }
        /// <summary>
        /// 加密私钥
        /// </summary>
        public static string PrivateKey
        {
            get { return settings["PrivateKey"]; }
        }
    }
}