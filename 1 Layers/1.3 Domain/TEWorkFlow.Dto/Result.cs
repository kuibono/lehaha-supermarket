using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEWorkFlow.Dto
{
    public class Result
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }

        public int Count { get; set; }

        public decimal Number { get; set; }

        public string Str { get; set; }
    }
}
