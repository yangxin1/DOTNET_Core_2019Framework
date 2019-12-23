using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    /// <summary>
    /// 基础数据访问接口
    /// </summary>
    public interface IBaseDAL<T>
    {
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        string TestMethod();

        List<string> GetList();

        /// <summary>
        /// 通过ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetDTOById(int id);

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Insert(T model);
    }
}
