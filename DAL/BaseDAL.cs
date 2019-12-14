using Dapper;
using IDAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    /// <summary>
    /// 基础访问类
    /// </summary>
    public class BaseDAL : IBaseDAL
    {
        static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly MySqlConnection _conn = new MySqlConnection(DBConfig.GetConn());
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        public string TestMethod()
        {
            try
            {
                return _conn.QueryFirstOrDefault<string>("select stu_name from test_student");
            } catch (Exception error)
            {
                Logger.Error("获取数据失败：" + error);
                return "";
            }
        }
        public List<string> GetList()
        {
            try
            {
                return _conn.Query<string>("select stu_name from test_student").ToList();
            }
            catch (Exception error)
            {
                Logger.Error("获取数据失败：" + error.Message);
                return null;
            }
        }
    }
}
