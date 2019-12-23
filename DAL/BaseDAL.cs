using Dapper;
using IDAL;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DAL
{
    /// <summary>
    /// 基础访问类
    /// </summary>
    public class BaseDAL<T> : IBaseDAL<T> where T : class, new()
    {
        #region 属性和字段
        /// <summary>
        /// 日志
        /// </summary>
        static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 连接字符串
        /// </summary>
        private readonly MySqlConnection _conn = new MySqlConnection(DBConfig.GetConn());

        /// <summary>
        /// 表类型
        /// </summary>
        //private readonly Type modeltype = typeof(T);
        private Type modeltype { get { return typeof(T); } }

        /// <summary>
        /// 表名
        /// </summary>
        //private readonly string tablename = GetTableName();
        private string tablename { get { return GetTableName(); } }

        /// <summary>
        /// 字段
        /// </summary>
        //private readonly string tablefields = Getfields();
        private string tablefields { get { return Getfields(); } }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        private  string GetTableName()
        {
            string tbname = "";
            Type t = typeof(T);
            TableAttribute tbattr = (TableAttribute)t.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault();
            tbname = tbattr.Name;
            return tbname;
        }
        /// <summary>
        /// 获取字段组合
        /// </summary>
        /// <returns></returns>
        private string Getfields()
        {
            string fields = " ";
            Type t = typeof(T);
            //字段名
            StringBuilder sqlfield = new StringBuilder();
            foreach (var x in t.GetProperties())
            {
                var linshi1 = x.CustomAttributes.FirstOrDefault().NamedArguments[0];
                string linshi2 = linshi1.TypedValue.Value.ToString();
                sqlfield.Append(linshi2);
                sqlfield.Append(",");
            }
            fields = sqlfield.ToString();
            fields = fields.Substring(0, fields.Length - 1);
            return fields;
        }
        #endregion


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
                return _conn.Query<string>("select stu_name from test_student limit 0,10").ToList();
            }
            catch (Exception error)
            {
                Logger.Error("获取数据失败：" + error.Message);
                return null;
            }
        }

        /// <summary>
        /// 通过ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetDTOById(int id)
        {
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.Append("select " + tablefields + " from " + tablename + " where id=" + id);
            T model = _conn.Query<T>(sqlsb.ToString()).FirstOrDefault();
            return model;
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool Insert(T model)
        {
            bool result = false;
            PropertyInfo[] propertys = modeltype.GetProperties();//获取属性
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into " + tablename + " (" + tablefields + ") values (");

            string fields = "";
            for (int i = 0; i < propertys.Length; i++)
            {
                if (propertys[i].PropertyType.FullName == "System.String" || propertys[i].PropertyType.FullName == "System.DateTime")//判断string类型加入引号
                {
                    fields += "'" + propertys[i].GetValue(model) + "',";
                }
                else
                {
                    fields += propertys[i].GetValue(model) + ",";
                }
            }
            fields = fields.Substring(0, fields.Length - 1);
            sb.Append(fields + ")");
            result = _conn.Execute(sb.ToString()) > 0;
            return result;
        }
    }
}
