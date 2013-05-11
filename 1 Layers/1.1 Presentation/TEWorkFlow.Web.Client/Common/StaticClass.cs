using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Collections;

namespace TEWorkFlow.Web.Client.Common
{
    public static class StaticClass
    {
        #region 转换为可空时间
        /// <summary>
        /// 转换为可空时间
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? ToNullAbleDateTime(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            try
            {
                DateTime result = Convert.ToDateTime(obj);
                if (result < new DateTime(1970, 1, 1))
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Json转换为对象
        public static object JsonDecode(this string json)
        {
            if (String.IsNullOrEmpty(json)) return "";
            object o = JsonConvert.DeserializeObject(json);
            if (o.GetType() == typeof(String) || o.GetType() == typeof(string))
            {
                o = JsonConvert.DeserializeObject(o.ToString());
            }
            object v = toObject(o);
            return v;
        }

        private static object toObject(object o)
        {
            if (o == null) return null;

            if (o.GetType() == typeof(string))
            {
                //判断是否符合2010-09-02T10:00:00的格式
                string s = o.ToString();
                if (s.Length == 19 && s[10] == 'T' && s[4] == '-' && s[13] == ':')
                {
                    o = System.Convert.ToDateTime(o);
                }
            }
            else if (o is JObject)
            {
                JObject jo = o as JObject;

                Hashtable h = new Hashtable();

                foreach (KeyValuePair<string, JToken> entry in jo)
                {
                    h[entry.Key] = toObject(entry.Value);
                }

                o = h;
            }
            else if (o is IList)
            {

                ArrayList list = new ArrayList();
                list.AddRange((o as IList));
                int i = 0, l = list.Count;
                for (; i < l; i++)
                {
                    list[i] = toObject(list[i]);
                }
                o = list;

            }
            else if (typeof(JValue) == o.GetType())
            {
                JValue v = (JValue)o;
                o = toObject(v.Value);
            }
            else
            {
            }
            return o;
        }

        #endregion
    }
}