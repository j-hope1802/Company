using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JobHistoryController
{
    private JobHistoryService   _JobHistoryService;
    public JobHistoryController(JobHistoryService JobHistoryService)
    {
        _JobHistoryService = JobHistoryService;
    }
    [HttpGet("GetJobHistory  ")]
    public async Task<Response<List<JobHistory  >>> GetJobHistory  ()=> await _JobHistoryService.GetJobHistory  ();

      [HttpPost("Insert")]
        public async Task<Response<int>> InsertJobHistory  (JobHistory   JobHistory  )
        {
            return await _JobHistoryService.InsertJobHistory  (JobHistory  );
        }

          [HttpPut("Update")]
        public async Task <Response<int>> UpdateJobHistory  (JobHistory   JobHistory  )
        {
            return await _JobHistoryService.UpdateJobHistory  (JobHistory  );
        }
           [HttpDelete]
         public async Task<Response<string>> DeleteJobHistory  (int id)
        {
            return await _JobHistoryService.DeleteJobHistory  (id);
        }

}
   