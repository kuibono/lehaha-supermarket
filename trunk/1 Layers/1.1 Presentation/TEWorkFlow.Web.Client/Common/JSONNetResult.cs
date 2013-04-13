using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;
using System.IO;
namespace TEWorkFlow.Web.Client.Common
{
    public class JsonNetResult : ActionResult
    {
        private readonly object _data;
        private readonly string _contentType;

        public JsonNetResult(object data,string contentType = "application/json")
        {
            _data = data;
            _contentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            TextWriter writer = new StringWriter();
            var s = new Newtonsoft.Json.JsonSerializer();
            s.Serialize(writer, _data);
            var response = context.HttpContext.Response;
            response.ContentType = _contentType;
            response.Write(writer.ToString());
        }
    }
}