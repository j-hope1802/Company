using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController
{
    private JobService _jobService;
    public JobController(JobService jobService)
    {
        _jobService = jobService;
    }
    [HttpGet("GetJob")]
    public async Task<Response<List<Job>>> GetJob()=> await _jobService.GetJob();

      [HttpPost("Insert")]
        public async Task<Response<int>> InsertJob(Job job)
        {
            return await _jobService.InsertJob(job);
        }

          [HttpPut("Update")]
        public async Task <Response<int>> UpdateJob(Job job)
        {
            return await _jobService.UpdateJob(job);
        }
           [HttpDelete]
         public async Task<Response<string>> DeleteJob(int id)
        {
            return await _jobService.DeleteJob(id);
        }

}
    