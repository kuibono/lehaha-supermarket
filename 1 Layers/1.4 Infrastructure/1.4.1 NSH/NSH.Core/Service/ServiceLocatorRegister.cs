using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSH.Core.Service
{
    /// <summary>
    /// 服务定位器注册器
    /// </summary>
    public class ServiceLocatorRegister
    {
        private static object _lockObject = new object();
        private static IServiceLocator _serviceLocator;

        public static IServiceLocator ServiceLocator
        {
            get
            {
                if (_serviceLocator == null)
                {
                    lock (_lockObject)
                    {
                        _serviceLocator = new LocalServiceLocator();
                    }
                }
                return _serviceLocator;
            }
        }
    }
}
