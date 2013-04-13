using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSH.Core.Service
{
    public interface IServiceLocator
    {
        object GetObject(string objectId);
    } 
}
