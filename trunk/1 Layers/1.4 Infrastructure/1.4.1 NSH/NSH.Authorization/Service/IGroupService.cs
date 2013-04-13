using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Authorization.Domain;

namespace NSH.Authorization.Service
{
    public interface IGroupService
    {
        /// <summary>
        /// 创建指定组
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Create(Group entity);

        /// <summary>
        /// 根据指定Id获取组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Group GetById(int id);

        /// <summary>
        /// 获取根用户组
        /// </summary>
        /// <returns></returns>
        Group GetRoot();

        /// <summary>
        /// 获取所有组
        /// </summary>
        /// <returns></returns>
        IList<Group> GetAll();

        /// <summary>
        /// 更新指定组
        /// </summary>
        /// <param name="entity"></param>
        void Update(Group entity);

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="entity"></param>
        void Delete(Group entity);

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<Group> entitys);
    }
}
