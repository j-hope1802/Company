using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

using Npgsql;

namespace Infrastructure.Services;

public class DepartmentService
{
    private readonly DapperContext _context;
    private readonly IWebHostEnvironment _hosting;

    public DepartmentService(DapperContext context,IWebHostEnvironment hosting)
    {
        _context = context;
        _hosting = hosting;
    }

     public async Task<Response<List<Department>>> GetDepartment()
        {
            using (var conn = _context.CreateConnection())

            {
                var con = await conn.QueryAsync<Department>("select d.department_id as DepartmentId, d.manager_id as ManagerId,l.location_id as LocationId,d.department_name  as DepartmentName from departments as d Join locations as l on l.location_id = d.location_id;");
                return new Response<List<Department>>(con.ToList());
            }
        }

        public async Task <Response<int>> InsertDepartment(Department department)
        {


 using (var conn = _context.CreateConnection())

            {
                 var sql =
                    $"insert into departments (department_name,manager_id,location_id) VALUES " +
                    $"('{department.DepartmentName}' ," +
                    $"{department.ManagerId} ," +
                    $"{department.LocationId} )" ;
                   
                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);

        }
        
}
   public async  Task<Response<int>> UpdateDepartment(Department department)
        {
 using (var conn = _context.CreateConnection())
            {
                var sql = 
                    $"UPDATE departments SET " +
                    $"Department_name = '{department.DepartmentName}', " +
                    $"manager_id= '{department.ManagerId}' ,"+ 
                     $"location_id= '{department.LocationId}' "+ 
                    $"WHERE department_id = {department.DepartmentId}";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
         public async Task<Response<string>> DeleteDepartment(int id)
        {
            using (var conn = _context.CreateConnection())
            {
               var response = await conn.ExecuteAsync($"delete from departments where Department_id= {id}");
            if(response > 0)
                return new Response<string>("Department deleted successfully");
        
            return new Response<string>(HttpStatusCode.BadRequest, "Department not found");
            }
        }
}
