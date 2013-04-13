using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain.WorkFlow;

namespace TEWorkFlow.Application.Service.WorkFlow
{
    public interface IInvoicePointService
    {
        /// <summary>
        /// 创建指定InvoicePoint
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Create(InvoicePoint entity);

        /// <summary>
        /// 根据指定Id获取InvoicePoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        InvoicePoint GetById(int id);

        /// <summary>
        /// 获取所有InvoicePoint
        /// </summary>
        /// <returns></returns>
        IList<InvoicePoint> GetAll();

        /// <summary>
        /// 更新指定InvoicePoint
        /// </summary>
        /// <param name="entity"></param>
        void Update(InvoicePoint entity);

        /// <summary>
        /// 删除指定InvoicePoint
        /// </summary>
        /// <param name="entity"></param>
        void Delete(InvoicePoint entity);

        /// <summary>
        /// 删除指定InvoicePoint
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<InvoicePoint> entitys);

        /// <summary>
        /// 根据InvoiceID和ItemName获取InvoicePoint
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        InvoicePoint GetByInvoiceIdAndItemName(int id, string itemName);
    }
}
