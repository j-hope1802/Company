using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController
{
    private DepartmentService _conService;
    public DepartmentController(DepartmentService conService)
    {
        _conService = conService;
    }
    [HttpGet("GetRegion")]
    public async Task<Response<List<Department>>> GetDepartment()=> await _conService.GetDepartment();

      [HttpPost("Insert")]
        public async Task<Response<int>> InsertDepartment(Department Department)
        {
            return await _conService.InsertDepartment(Department);
        }

          [HttpPut("Update")]
        public async Task <Response<int>> UpdateDepartment(Department Department)
        {
            return await _conService.UpdateDepartment(Department);
        }
           [HttpDelete]
         public async Task<Response<string>> DeleteDepartment(int id)
        {
            return await _conService. DeleteDepartment(id);
        }

}
    