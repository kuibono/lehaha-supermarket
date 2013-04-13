using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;

namespace NSH.Core.Service
{
    public class LocalServiceLocator : IServiceLocator
    {
        private IApplicationContext _applicationContext;

        public IApplicationContext ApplicationContext
        {
            get
            {
                return _applicationContext
                    ?? (_applicationContext = ContextRegistry.GetContext());
            }
        }

        public object GetObject(string objectId)
        {
            try
            {
                return ApplicationContext.GetObject(objectId);
            }
            catch
            {
                return null;
            }
        }
    }
}
