using DTO.Model.Student;
using IDAL.ITestStudent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sparkle_Framework2019.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sparkle_Framework2019.Controllers.TestStudentController
{
    /// <summary>
    /// 学生信息相关接口（测试）
    /// </summary>
    [ApiController]
    public class TestStudentController : BaseAPIController
    {
        /// <summary>
        /// 服务
        /// </summary>
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
        [Authorize]
        [HttpGet("/api/test/student{id}")]
        public IActionResult GetStudentById(int id)
        {
            TestStudentDTO student = service.GetDTOById(id);
            return Success(student);
        }

        /// <summary>
        /// 获取学生ID 异步
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/test/getstudentasync{id}")]
        public async Task<IActionResult> Getstuasync(int id)
        {
            TestStudentDTO stu = await service.GetDTOByIdAsync(id);
            return Success(stu);
        }
        /// <summary>
        /// 获取学生列表
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="limit">每页数量</param>
        /// <returns></returns>
        [HttpGet("/api/test/studentlist")]
        public IActionResult GetStudentList(int index=1,int limit = 10)
        {
            List<TestStudentDTO> stulist = service.GetList(index, limit);
            return Success(stulist);
        }
        /// <summary>
        /// 获取学生列表异步
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="limit">每页数量</param>
        /// <returns></returns>
        [HttpGet("/api/test/studentlistasync")]
        public async Task<IActionResult> GetStudentListAsync(int index = 1, int limit = 10)
        {
            List<TestStudentDTO> list = await service.GetListAsync(index, limit);
            return Success(list);
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
        /// <summary>
        /// 编辑学生信息
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        [HttpPut("/api/test/student")]
        public IActionResult UpdateStudent(TestStudentDTO stu)
        {
            if (service.Update(stu))
            {
                return Success();
            }
            else
            {
                return Fail("修改失败");
            }
        }
        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/api/test/student/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            if (service.Delete(id))
            {
                return Success();
            }
            else
            {
                return Fail("删除失败");
            }
        }
    }
}
