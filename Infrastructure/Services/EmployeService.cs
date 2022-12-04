using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Services;

public class EmployeeService
{
    private DapperContext _context;

 private readonly IWebHostEnvironment _hostEnvironment;

    public EmployeeService(DapperContext context,IWebHostEnvironment env)
    {
        _context = context;
        _hostEnvironment = env;
    }
    

    public async Task<Response<List<GetEmployee>>> GetEmployees()
    {
       using (var conn = _context.CreateConnection())
        {
            var sql = $"select  e.employe_id as employeeid, e.first_name as firstname,e.last_name as lastname, e.email,e.phone_number as phonenumber, d.department_name as departmentname, e.comission,e.sallary,e.hire_date from employees as e join departments as d on e.departament_id  = d.department_id " ;
            var  result = await conn.QueryAsync<GetEmployee>(sql);
            return new Response<List<GetEmployee>>(result.ToList());
            
        }
    }
    public async Task<GetEmployee> AddPhoto(GetEmployee emg)
    {
        var path = Path.Combine(_hostEnvironment.WebRootPath, "images");
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
        
        var filePath = Path.Combine(path, emg.ProfileImage.FileName);
        using (var stream = File.Create(filePath))
        {
            await emg.ProfileImage.CopyToAsync(stream);
        }
        


        return emg;
    }


    public async Task<Response<int>> InsertEmployee(GetEmployee emp)
    {
      using (var conn = _context.CreateConnection())
        {
             var path = Path.Combine(_hostEnvironment.WebRootPath, "images",emp.ProfileImage.FileName);
            
            using (var stream = File.Create(path))
            {
                await emp.ProfileImage.CopyToAsync(stream);
            }
            var sql = 
              $"insert into employees (first_name,last_name,email,phone_number,departament_id,comission,sallary,hire_date) values " +
                                        $"('{emp.FirstName} '," + 
                                         $"'{emp.LastName}', " + 
                                             $"'{emp.PhoneNumber}', " + 
                                            $"'{emp.Email}', " +
                                             $"{emp.DepartmentId}, " + 
                                         $"'{emp.Comission}', " + 
                                            $"{emp.Sallary}, " + 
                                          $"'{emp.HireDate}' )";
            var result = await conn.ExecuteAsync(sql);
            
            return new Response<int>(result);
            
        }
    }
        public async Task<Response<int>> UpdateEmployee(Employee emp)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = 
              $"Update employees set " +
              $"first_name =  '{emp.FirstName} '," + 
              $"'last_name = '{emp.LastName}', " + 
              $" email = '{emp.Email}', " + 
              $"phone_number = phone_number = '{emp.PhoneNumber}', " + 
              $"departament_id = {emp.DepartmentId}, " + 
              $"commision = '{emp.Comission}', " + 
              $"salary = {emp.Sallary}, " + 
              $"hire_date = '{emp.HireDate}' " +
              $"where employe_id = {emp.EmployeeId}" ;



                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
        public async Task<Response<string>> DeleteEmployee(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"DELETE FROM Employees WHERE employe_id = {id} ";

                var response = await conn.ExecuteAsync(sql); 


                if(response > 0)
                return new Response<string>("Employee deleted successfully");
        
            return new Response<string>(HttpStatusCode.BadRequest, "employee not found");

              
            }
        }
}