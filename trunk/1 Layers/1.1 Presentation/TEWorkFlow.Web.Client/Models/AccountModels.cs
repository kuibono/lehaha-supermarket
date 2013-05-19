using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NSH.Core.Domain;

namespace TEWorkFlow.Web.Client.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Required]
        [Display(Name = "V Code")]
        public string VCode { get; set; }

        [Display(Name = "logintype")]
        public int logintype { get; set; }

    }

    public class JsonTreeModel
    {
        public int id { get; set; }

        public string text { get; set; }

        public string path { get; set; }

        public string icon { get; set; }

        public int count { get; set; }

        public bool issystem { get; set; }

        public string remark { get; set; }

        public bool @checked { get; set; }

        public int parentid { get; set; }

        public JsonTreeModel[] children { get; set; }

        /// <summary>
        /// 转换为JsonTreeModel数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">数据</param>
        /// <param name="setPropertyAction">自定义属性赋值</param>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public static JsonTreeModel[] Convert<T>(IEnumerable<T> data,
            Action<T, JsonTreeModel> setPropertyAction = null, Func<T, bool> predicate = null)
            where T : ITreeEntity<T>
        {
            var list = new List<JsonTreeModel>();
            if (predicate != null)
            {
                data = data.Where(predicate);
            }
            foreach (T d in data)
            {
                var j = new JsonTreeModel();
                if (setPropertyAction != null)
                {
                    setPropertyAction(d, j);
                }
                j.children = (d.Children != null && d.Children.Count > 0) ? Convert(d.Children, setPropertyAction, predicate) : null;
                list.Add(j);
            }
            return list.ToArray();
        }
    }
}