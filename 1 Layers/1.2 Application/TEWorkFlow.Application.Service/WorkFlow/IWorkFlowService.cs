using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain.WorkFlow;

namespace TEWorkFlow.Application.Service.WorkFlow
{
    public interface IWorkFlowService
    {
        /// <summary>
        /// 创建指定WorkFlow
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Create(TEWorkFlow.Domain.WorkFlow.WorkFlow entity);

        /// <summary>
        /// 根据指定Id获取WorkFlow
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEWorkFlow.Domain.WorkFlow.WorkFlow GetById(int id);

        /// <summary>
        /// 获取所有WorkFlow
        /// </summary>
        /// <returns></returns>
        IList<TEWorkFlow.Domain.WorkFlow.WorkFlow> GetAll();

        /// <summary>
        /// 更新指定WorkFlow
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEWorkFlow.Domain.WorkFlow.WorkFlow entity);

        /// <summary>
        /// 删除指定WorkFlow
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEWorkFlow.Domain.WorkFlow.WorkFlow entity);

        /// <summary>
        /// 删除指定WorkFlow
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<TEWorkFlow.Domain.WorkFlow.WorkFlow> entitys);
    }
}
