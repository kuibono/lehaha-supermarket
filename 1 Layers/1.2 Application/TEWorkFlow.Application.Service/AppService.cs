using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace TEWorkFlow.Application.Service
{
    public class AppService : IAppService
    {
        public ISessionFactory SessionFactory { get; set; }

        #region IAppService Members

        public void Load()
        {
        }

        public void Unload()
        {
            if (SessionFactory != null)
            {
                try { SessionFactory.Close(); }
                catch { }
            }
        }

        #endregion
    }
}
