using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

using Npgsql;

namespace Infrastructure.Services;

public class JobHistoryService
{
    private readonly DapperContext _context;
    private readonly IWebHostEnvironment _hosting;

    public JobHistoryService(DapperContext context,IWebHostEnvironment hosting)
    {
        _context = context;
        _hosting = hosting;
    }

     public async Task<Response<List<JobHistory>>> GetJobHistory()
        {
            using (var conn = _context.CreateConnection())

            {
                var con = await conn.QueryAsync<JobHistory>("select e.employe_id as EmployeId, jh.start_date as StartDate,jh.end-date  as EndDate,j.job_id  as JobId,d.department_id as DepartmentId  from Job_History as jh Join jobs as j on j.job_id = jh.job_id join departments as d on d.department_id =jh.department_id;");
                return new Response<List<JobHistory  >>(con.ToList());
            }
        }

        public async Task <Response<int>> InsertJobHistory  (JobHistory jobhistory  )
        {


 using (var conn = _context.CreateConnection())

            {
                 var sql =
                    $"insert into Job_History   (employe_id,start_date,end_date,job_id,department_id) VALUES " +
                    $"({jobhistory.EmployeId} ," +
                    $"{jobhistory  .StartDate.ToString("yyyy-MM-dd")} ," +
                       $"{jobhistory  .JobId} ," +
                        $"{jobhistory  .DepartmentId} ," +
                    $"{jobhistory  .EndDate.ToString("yyyy-MM-dd")} )" ;
                   
                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);

        }
        
}
   public async  Task<Response<int>> UpdateJobHistory  (JobHistory   jobhistory  )
        {
 using (var conn = _context.CreateConnection())
            {
                var sql = 
                    $"UPDATE job_history   SET " +
                    $"employe_id= '{jobhistory  .EmployeId}', " +
                    $"start_date= '{jobhistory  .StartDate.ToString("yyyy-MM-dd")}' ,"+ 
                     $"end_date= '{jobhistory  .EndDate.ToString("yyyy-MM-dd")}' ,"+
                     $"job_id= '{jobhistory  .JobId}' ,"+
                     $"department_id= '{jobhistory  .DepartmentId}' "+ 
                    $"WHERE employe _id = {jobhistory.EmployeId}";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
         public async Task<Response<string>> DeleteJobHistory  (int id)
        {
            using (var conn = _context.CreateConnection())
            {
               var response = await conn.ExecuteAsync($"delete from Job_History  where employe_id= {id}");
            if(response > 0)
                return new Response<string>("JobHistory   deleted successfully");
        
            return new Response<string>(HttpStatusCode.BadRequest, "JobHistory   not found");
            }
        }
}
