using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;

namespace TEWorkFlow.Domain.Sys
{
    public class SysLoginPower : EntityGUIDBase, IAggregateRootGUID
    {
        public virtual string Emname { get; set; }
        public virtual string Username { get; set; }
        public virtual string Userpw { get; set; }
        public virtual string Ifcash { get; set; }
        public virtual string Cashfunction { get; set; }
        public virtual string Userstate { get; set; }
        public virtual decimal? Maxdiscount { get; set; }
        public virtual int? UserType { get; set; }

        public virtual string UserTypeName
        {
            get
            {
                if(UserType==0)
                {
                    return "总店";
                }
                else if(UserType==1)
                {
                    return "分店";
                }
                else
                {
                    return "供应商";
                }
            }
        }

        protected override void Validate()
        {
        }
    }
}
