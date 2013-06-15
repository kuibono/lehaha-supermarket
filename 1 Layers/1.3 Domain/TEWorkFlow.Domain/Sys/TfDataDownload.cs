/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/6/15 13:47:26
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Sys
{
    ///<summary>
    ///表tf_data_download的实体类
    ///</summary>
    public class TfDataDownload : EntityGUIDBase, IAggregateRootGUID
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string DownloadTablename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string DownloadBranchcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string DownloadKeyvalue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int DownloadOrder { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public TfDataDownload Clone()
        {
            return (TfDataDownload)this.MemberwiseClone();
        }
    }

}


