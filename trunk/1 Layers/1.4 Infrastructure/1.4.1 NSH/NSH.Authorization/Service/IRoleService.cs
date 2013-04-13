using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Authorization.Domain;

namespace NSH.Authorization.Service
{
    public interface IRoleService
    {
        /// <summary>
        /// 创建指定角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Create(Role entity);

        /// <summary>
        /// 根据指定Id获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Role GetById(int id);

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        IList<Role> GetAll();

        /// <summary>
        /// 更新指定角色
        /// </summary>
        /// <param name="entity"></param>
        void Update(Role entity);

        /// <summary>
        /// 删除指定角色
        /// </summary>
        /// <param name="entity"></param>
        void Delete(Role entity);

        /// <summary>
        /// 删除指定角色
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<Role> entitys);
    }
}
