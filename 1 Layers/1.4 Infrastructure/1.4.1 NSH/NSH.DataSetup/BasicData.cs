using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using NSH.Authorization.Domain;
using TEWorkFlow.Domain.Sys;

namespace NSH.DataSetup
{
    public class BasicData
    {
        public void InitialUser()
        {
            SysLoginPower u=new SysLoginPower();
            u.Emname = "管理员";
            u.Id = "default";
            u.Ifcash = "false";
            u.Maxdiscount = 0;
            u.UserType = 0;
            u.Username = "sysadmin";
            u.Userpw = "1";
            u.Userstate = "0";
        }
        public void InitialModule()
        {
            
        }

        public void InitialRole()
        {
            
        }

    }
}
