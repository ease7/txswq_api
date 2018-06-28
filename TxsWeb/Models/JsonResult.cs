using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TxsWeb.Models
{
    public class JsonRes : JsonResult
    {
        public JsonRes(object value)
        {
            base.Data = value;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "text/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {

                string rtn = SerializerByNewton(Data);
                Data = null;
                response.Write(rtn);
            }
        }

        public string dateformat { get; set; }

        /// <summary>
        /// 属性名称小写字母转换
        /// </summary>
        public bool IsLowerPropertyName { get; set; }

        public string[] ignoreproperties { get; set; }

        /// <summary>
        /// 重命名属性名map
        /// </summary>
        public Dictionary<string, string> NameMapping { get; set; }

        public string SerializerByNewton(object obj)
        {
            if (obj == null)
                return "";
            JsonSerializerSettings js = new JsonSerializerSettings();
            js.NullValueHandling = NullValueHandling.Ignore;
            js.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
     
            IsoDateTimeConverter t = new IsoDateTimeConverter();
            if (string.IsNullOrEmpty(dateformat))
            {
                dateformat = "yyyy-MM-dd HH:mm:ss";
            }
            t.DateTimeFormat = dateformat;
            js.Converters.Add(t);
            return JsonConvert.SerializeObject(obj, Formatting.None, js);
        }

        /// <summary>
        /// 所有属性名小写
        /// </summary>
        /// <returns></returns>
        public JsonRes Lower()
        {
            this.IsLowerPropertyName = true;
            return this;
        }

        /// <summary>
        /// 忽略属性名
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public JsonRes Ignore(params string[] names)
        {
            this.ignoreproperties = names;
            return this;
        }

        /// <summary>
        /// 所有的时间格式化
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public JsonRes SetDateFormat(string format)
        {
            this.dateformat = format;
            return this;
        }

        /// <summary>
        /// 重命名属性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public JsonRes RenameProperty(string name, string replace)
        {
            if (NameMapping == null)
            {
                NameMapping = new Dictionary<string, string>();
            }

            if (!NameMapping.Keys.Contains(name))
            {
                NameMapping.Add(name, replace);
            }

            return this;
        }
    }
}