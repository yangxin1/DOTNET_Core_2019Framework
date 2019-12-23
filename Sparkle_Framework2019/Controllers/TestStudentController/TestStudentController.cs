using DTO.Model.Student;
using IDAL.ITestStudent;
using Microsoft.AspNetCore.Mvc;
using Sparkle_Framework2019.Controllers.Base;

namespace Sparkle_Framework2019.Controllers.TestStudentController
{
    public class TestStudentController:BaseAPIController
    {
        private readonly ITestStudent service;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Service"></param>
        public TestStudentController(ITestStudent Service)
        {
            service = Service;
        }
        /// <summary>
        /// 获取学生ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/test/student{id}")]
        public IActionResult GetStudentById(int id)
        {
            TestStudentDTO student = service.GetDTOById(id);
            return Success(student);
        }
        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        [HttpPost("/api/test/student")]
        public IActionResult InsertStuddent(TestStudentDTO stu)
        {
            if (service.Insert(stu))
            {
                return Success();
            }
            else
            {
                return Fail("添加学生失败");
            }
        }
    }
}
