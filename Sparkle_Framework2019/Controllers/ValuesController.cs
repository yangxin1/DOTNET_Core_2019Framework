﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using IDAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NLog;
using Sparkle_Framework2019.Controllers.Base;

namespace Sparkle_Framework2019.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseAPIController
    {
        /// <summary>
        /// 加密linshi
        /// </summary>
        //private readonly IDESHelper _des;
        //private readonly IBaseDAL dal;
        //public ValuesController(IDESHelper Deshelper,IBaseDAL DAL)
        //{
        //    dal = DAL;
        //    _des = Deshelper;
        //}
        ///// <summary>
        ///// 日志测试
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    Logger.Info("这是INFO测试");
        //    Logger.Error("这是Error测试");
        //    Logger.Debug("这是Debug测试");
        //    return new string[] { "value1", "value2" };
        //}

        ///// <summary>
        ///// 获取加密字符
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("/api/values/des")]
        //public IActionResult GetDES()
        //{
        //    return Success( _des.Encrypt("Server=www.sparkle831143.cn;Database=test_db; Uid =root;Pwd=sparkle831; Port =3306;SslMode=none"));
        //}
        ///// <summary>
        ///// 获取解密字符
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("/api/values/endes")]
        //public IActionResult GetEndes(string des)
        //{
        //    return Success(_des.Decrypt(des));
        //}
        ///// <summary>
        ///// 获取姓名数据
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("/api/values/name")]
        //public IActionResult GetData()
        //{
        //    Logger.Info("数据库获取数据");
        //    return Success(dal.GetList());
        //}
    }
}
