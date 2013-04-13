using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain.WorkFlow;
using NSH.Authorization.Domain;

namespace TEWorkFlow.Application.Service.WorkFlow
{
    public interface IWorkStepService
    {
        /// <summary>
        /// 创建指定Status
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Create(WorkStep entity);

        /// <summary>
        /// 根据指定Id获取Status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        WorkStep GetById(int id);

        /// <summary>
        /// 获取所有Status
        /// </summary>
        /// <returns></returns>
        IList<WorkStep> GetAll();


        /// <summary>
        /// 根据指定用户获取WorkStep
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        WorkStep GetByUser(User user);

        /// <summary>
        /// 更新指定Status
        /// </summary>
        /// <param name="entity"></param>
        void Update(WorkStep entity);

        /// <summary>
        /// 删除指定Status
        /// </summary>
        /// <param name="entity"></param>
        void Delete(WorkStep entity);

        /// <summary>
        /// 删除指定Status
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<WorkStep> entitys);
    }
}
