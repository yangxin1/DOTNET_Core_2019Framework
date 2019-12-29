using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Model.DTOCommon
{
    /// <summary>
    /// DTO基础基类
    /// </summary>
    public class BaseSimpleEntity
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [DTOColumn(Name = "id")]
        public int Id { get; set; }
    }
}
