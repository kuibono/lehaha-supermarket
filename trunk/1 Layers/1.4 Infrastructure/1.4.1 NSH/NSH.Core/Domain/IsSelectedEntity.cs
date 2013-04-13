using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSH.Core.Domain
{
    public interface IsSelectedEntity
    {
        bool? IsSelected { get; set; }
    }
}
