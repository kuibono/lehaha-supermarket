using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEWorkFlow.Web.Client.Models
{
    public class JsonMessage
    {
        public const string ContentType = "application/json";

        public bool IsLogon { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public JsonMessage()
        {
        }

        public JsonMessage(bool sucess, string message = null)
        {
            this.Success = sucess;
            this.Message = message;
        }

        public JsonMessage AsSuccess(string message = "")
        {
            this.Success = true;
            this.Message = message;
            return this;
        }

        public JsonMessage AsFail(string message = "")
        {
            this.Success = false;
            this.Message = message;
            return this;
        }
    }

    public class JsonNameValue
    {
        public JsonNameValue(string text, string value, string typeName = "")
        {
            this.Text = text;
            this.Value = value;
            this.TypeName = typeName;
        }

        public string Text { get; set; }

        public string Value { get; set; }

        public string TypeName { get; set; }
    }
}