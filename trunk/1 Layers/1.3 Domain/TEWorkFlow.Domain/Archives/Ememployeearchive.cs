using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;

namespace TEWorkFlow.Domain.Archives
{
    public class Ememployeearchive : EntityGUIDBase, IAggregateRootGUID
    {
        public virtual string Emname { get; set; }

        public virtual string Nameused { get; set; }
        public virtual DateTime? Birthday { get; set; }
        public virtual string Sex { get; set; }
        public virtual string Politicscode { get; set; }
        public virtual string Nationcode { get; set; }
        public virtual string Educationcode { get; set; }
        public virtual string Professionaltitlecode { get; set; }
        public virtual string Ifmarried { get; set; }
        public virtual string Nativeplace { get; set; }
        public virtual string Idcard { get; set; }
        public virtual DateTime? Accededay { get; set; }
        public virtual string Familyphone { get; set; }
        public virtual string Handset { get; set; }
        public virtual string Email { get; set; }
        public virtual string Familyaddress { get; set; }
        public virtual string Postalcode { get; set; }
        public virtual string Graduateacademy { get; set; }
        public virtual DateTime? Graduateday { get; set; }
        public virtual string Dcode { get; set; }
        public virtual string Bcode { get; set; }
        public virtual string Dutycode { get; set; }
        public virtual string Bankaccount { get; set; }
        public virtual string Pensionaccount { get; set; }
        public virtual string Emstate { get; set; }
        public virtual decimal? Stature { get; set; }
        public virtual decimal? Width { get; set; }

        public virtual string LoginName { get; set; }
        public virtual string LoginPass { get; set; }
        public virtual string Remark { get; set; }

        public virtual string SexString
        {
            get { return Sex == "0" ? "男" : "女"; }
        }

        public virtual string Marriage
        {
            get { return Ifmarried == "0" ? "未婚" : "已婚"; }
        }

        public virtual string PoliticsName { get; set; }




        protected override void Validate()
        {
        }
    }
}
