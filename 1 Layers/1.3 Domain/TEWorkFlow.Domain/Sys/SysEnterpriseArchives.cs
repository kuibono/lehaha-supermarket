/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/5/19 19:18:50
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Sys
{
    ///<summary>
    ///表sys_enterprise_archives的实体类
    ///</summary>
    public class SysEnterpriseArchives : EntityGUIDBase, IAggregateRootGUID
    {

        /// <summary>
        /// 企业名称
        /// </summary>
        public virtual string EnName { get; set; }

        /// <summary>
        /// 公司简称
        /// </summary>
        public virtual string EnComName { get; set; }

        /// <summary>
        /// 企业类型
        /// </summary>
        public virtual string EnType { get; set; }

        /// <summary>
        /// 企业性质
        /// </summary>
        public virtual string EnCharacter { get; set; }

        /// <summary>
        /// 经营面积
        /// </summary>
        public virtual decimal DealArea { get; set; }

        /// <summary>
        /// 注册地址
        /// </summary>
        public virtual string RegisterAddress { get; set; }

        /// <summary>
        /// 系统版本
        /// </summary>
        public virtual string CurrentVersion { get; set; }

        /// <summary>
        /// 最后更新版本
        /// </summary>
        public virtual string LastVersion { get; set; }


        public virtual string TempString { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public SysEnterpriseArchives Clone()
        {
            return (SysEnterpriseArchives)this.MemberwiseClone();
        }
    }

}


