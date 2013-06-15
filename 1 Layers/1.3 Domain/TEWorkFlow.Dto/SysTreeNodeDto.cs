using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEWorkFlow.Dto
{
    public class SysTreeNodeDto
    {
        public string id { get; set; }

        public string text { get; set; }

        public string pid { get; set; }

        public string iconCls { get; set; }

        public string url { get; set; }

        public bool newwindow { get; set; }
    }
}
