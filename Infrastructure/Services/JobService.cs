using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

using Npgsql;

namespace Infrastructure.Services;

public class JobService
{
    private readonly DapperContext _context;
    private readonly IWebHostEnvironment _hosting;

    public JobService(DapperContext context,IWebHostEnvironment hosting)
    {
        _context = context;
        _hosting = hosting;
    }

     public async Task<Response<List<Job>>> GetJob()
        {
            using (var conn = _context.CreateConnection())

            {
                var job = await conn.QueryAsync<Job>("SELECT max_salary as MaxSalary , min_salary as MinSalary,job_title as jobTitle,job_id as JobId  from jobs");
                return new Response<List<Job>>(job.ToList());
            }
        }

        public async Task <Response<int>> InsertJob(Job job)
        {


 using (var conn = _context.CreateConnection())

            {
                 var sql =
                    $"insert into jobs (job_title,max_salary,min_salary) VALUES " +
                    $"('{job.JobTitle}' ," +
                     $"{job.MaxSalary} ," +
                    $"{job.MinSalary} )" ;
                   
                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);

        }
        
}
   public async  Task<Response<int>> UpdateJob(Job job)
        {
 using (var conn = _context.CreateConnection())
            {
                var sql = 
                    $"UPDATE jobs SET " +
                     $"job_title = '{job.JobTitle}', " +
                    $"max_salary = '{job.MaxSalary}', " +
                    $"min_salary = '{job.MinSalary}' "+ 
                    $"WHERE job_id = {job.JobId}";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
         public async Task<Response<string>> DeleteJob(int id)
        {
            using (var conn = _context.CreateConnection())
            {
               var response = await conn.ExecuteAsync($"delete from jobs where job_id= {id}");
            if(response > 0)
                return new Response<string>("Jobs deleted successfully");
        
            return new Response<string>(HttpStatusCode.BadRequest, "job not found");
            }
        }
}
