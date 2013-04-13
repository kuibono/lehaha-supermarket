using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TEWorkFlow.Domain.WorkFlow;
using NSH.Core.DataPage;
using NSH.Authorization.Domain;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Application.Service.WorkFlow
{
    public interface IInvoiceService
    {
        /// <summary>
        /// 创建指定发票
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Create(Invoice entity);

        /// <summary>
        /// 根据指定Id获取发票
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Invoice GetById(int id);

        /// <summary>
        /// 获取所有发票
        /// </summary>
        /// <returns></returns>
        IList<Invoice> GetAll();

        /// <summary>
        /// 根据用户获取发票列表
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataPage"></param>
        /// <param name="dataOrder"></param>
        /// <returns></returns>
        IList<Invoice> GetListByUser(User user, IDataPage dataPage = null, IDataOrder dataOrder = null, bool onlyUsersPoints = false);

        /// <summary>
        /// 根据关键词获取发票列表
        /// </summary>
        /// <param name="field"></param>
        /// <param name="keyword"></param>
        /// <param name="dataPage"></param>
        /// <param name="dataOrder"></param>
        /// <returns></returns>
        IList<Invoice> GetListByKeyword(string field, string keyword, IDataPage dataPage = null, IDataOrder dataOrder = null);

        /// <summary>
        /// 根据关键词字典获取发票列表
        /// </summary>
        /// <param name="fieldValues"></param>
        /// <param name="dataPage"></param>
        /// <param name="dataOrder"></param>
        /// <returns></returns>
        IList<Invoice> GetListByKeyword(IDictionary<string, string> fieldValues, IDataPage dataPage = null, IDataOrder dataOrder = null);

        /// <summary>
        /// 根据Dto获取发票列表
        /// </summary>
        /// <param name="searchDto"></param>
        /// <param name="dataPage"></param>
        /// <param name="dataOrder"></param>
        /// <returns></returns>
        IList<Invoice> GetListByDto(InvoiceSearchDto searchDto, IDataPage dataPage = null, IDataOrder dataOrder = null);

        /// <summary>
        /// 更新指定发票
        /// </summary>
        /// <param name="entity"></param>
        void Update(Invoice entity);

        /// <summary>
        /// 删除指定发票
        /// </summary>
        /// <param name="entity"></param>
        void Delete(Invoice entity);

        /// <summary>
        /// 删除指定发票
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<Invoice> entitys);

        /// <summary>
        /// 检察状态并更新
        /// </summary>
        /// <param name="id"></param>
        void CheckFinished(int id);


    }
}
