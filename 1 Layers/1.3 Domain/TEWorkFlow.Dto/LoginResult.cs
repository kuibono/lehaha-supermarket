using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain.Archives;

namespace TEWorkFlow.Dto
{
    public class LoginResult
    {
        public bool IsSuccess { get; set; }

        public Ememployeearchive Employee { get; set; }

        public FbSupplierArchives Supplier { get; set; }

        public string Text { get; set; }
    }
}
