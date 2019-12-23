using Dapper;
using DTO.Model.Student;

namespace DTO.Model.DTOCommon
{
    public class ColumnMapper
    {
        /// <summary>
        /// 实体映射类
        /// </summary>
        public static void SetMapper()
        {
            //数据库字段名和c#属性名不一致，手动添加映射关系
            SqlMapper.SetTypeMap(typeof(TestStudentDTO), new DTOColumnAttributeTypeMapper<TestStudentDTO>());
            
        }
    }
}
