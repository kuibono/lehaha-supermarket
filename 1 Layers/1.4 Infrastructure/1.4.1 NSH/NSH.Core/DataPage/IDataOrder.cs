using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSH.Core.DataPage
{
    public interface IDataOrder
    {
        /// <summary>
        /// order field
        /// </summary>
        string By { get; set; }

        /// <summary>
        /// asc or desc
        /// </summary>
        string Order { get; set; }
    }
}
