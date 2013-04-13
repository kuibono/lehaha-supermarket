using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Authorization.Domain;
using System.Linq.Expressions;

namespace NSH.Authorization.Service
{
    public interface IModuleService
    {
        /// <summary>
        /// 创建指定模块
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Create(Module entity);

        /// <summary>
        /// 根据指定Id获取模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Module GetById(int id);

        /// <summary>
        /// 根据指定名称获取模块
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Module GetByName(string name);

        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        IList<Module> GetAll();

        /// <summary>
        /// 更新指定模块
        /// </summary>
        /// <param name="entity"></param>
        void Update(Module entity);

        /// <summary>
        /// 根据条件更新模块
        /// </summary>
        /// <param name="updater"></param>
        /// <param name="predicate"></param>
        void WhereUpdate(Action<Module> updater,
        Expression<Func<Module, bool>> predicate = null);

        /// <summary>
        /// 删除指定模块
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IList<Module> entitys);
    }
}
