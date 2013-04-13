using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain.WorkFlow;

namespace TEWorkFlow.Application.Service.WorkFlow
{
    public interface ICaseService
    {
        /// <summary>
        /// 创建指定Case
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Create(Case entity);

        /// <summary>
        /// 根据指定Id获取Case
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Case GetById(int id);

        /// <summary>
        /// 获取所有Case
        /// </summary>
        /// <returns></returns>
        IList<Case> GetAll();

        /// <summary>
        /// 更新指定Case
        /// </summary>
        /// <param name="entity"></param>
        void Update(Case entity);

        /// <summary>
        /// 删除指定Case
        /// </summary>
        /// <param name="entity"></param>
        void Delete(Case entity);

        /// <summary>
        /// 删除指定Case
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<Case> entitys);
    }
}
