using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEWorkFlow.Dto
{
    public class InvoiceSearchDto
    {
        public int? stepid {get; set;}

        public int? userid {get; set;}

        public string field {get; set;}

        public string fieldType {get; set;}

        public string keyword {get; set;}

        public DateTime? dateStart {get; set;}

        public DateTime? dateEnd {get; set;}
    }
}
