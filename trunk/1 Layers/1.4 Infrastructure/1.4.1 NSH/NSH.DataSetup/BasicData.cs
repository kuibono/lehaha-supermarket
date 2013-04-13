using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Authorization.Domain;

namespace NSH.DataSetup
{
    public class BasicData
    {
        public void InitialUser()
        {
            
        }
        public void InitialModule()
        {
            Module m=new Module();
            m.ModuleName = "root";
            m.DisplayOrder = -1;
            m.IsMDI = false;
            m.IsEnable = true;
            m.Parent = null;
            m.Path = null;



        }

        public void InitialRole()
        {
            
        }

    }
}
