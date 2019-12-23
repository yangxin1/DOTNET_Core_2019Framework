using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sparkle_Framework2019.Controllers.Base;

namespace Sparkle_Framework2019.Controllers.Login
{
    /// <summary>
    /// 登陆控制器
    /// </summary>
    [ApiController]
    public class LoginController : BaseAPIController
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/login/login")]
        public IActionResult Login()
        {
            //JWT
            return Success();
        }
    }
}