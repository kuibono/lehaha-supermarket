using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain.WorkFlow;
using NSH.Authorization.Domain;

namespace TEWorkFlow.Application.Service.WorkFlow
{
    public interface ICheckPointService
    {
        /// <summary>
        /// 创建指定CheckPoint
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Create(CheckPoint entity);

        /// <summary>
        /// 根据指定Id获取CheckPoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CheckPoint GetById(int id);

        /// <summary>
        /// 获取所有CheckPoint
        /// </summary>
        /// <returns></returns>
        IList<CheckPoint> GetAll();

        /// <summary>
        /// 根据指定WorkStep获取CheckPoint列表
        /// </summary>
        /// <param name="workstep"></param>
        /// <returns></returns>
        IList<CheckPoint> GetListByWorkstep(WorkStep workstep);

        /// <summary>
        /// 根据指定用户获取CheckPoint列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IList<CheckPoint> GetListByUser(User user, bool? iskey = false);

        /// <summary>
        /// 更新指定CheckPoint
        /// </summary>
        /// <param name="entity"></param>
        void Update(CheckPoint entity);

        /// <summary>
        /// 删除指定CheckPoint
        /// </summary>
        /// <param name="entity"></param>
        void Delete(CheckPoint entity);

        /// <summary>
        /// 删除指定CheckPoint
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<CheckPoint> entitys);
    }
}
