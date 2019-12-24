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
        public IActionResult Login(string name,string password)
        {
            //JWT
            //身份校验
            //存入缓存
            string roles = "admin";
            Dictionary<string, object> para = new Dictionary<string, object>
            {
                { "name", name },
                { "password", password },
                { "logintime", DateTime.Now },
                { "roles",roles }
            };
            string token = Token.CreateTokenByHandler(para); // 加密
            return Success(token);
        }
        /// <summary>
        /// 检测登录
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        [HttpPost("/api/login/checklogin")]
        public IActionResult CheckLogin(string jwt)
        {
            if(Token.Validate(jwt, out string message))
            {
                return Success(CurrentUserName);
            }
            else
            {
                return Fail("验证失败：" + message);
            }
        }
    }
}