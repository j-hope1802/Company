using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController
{
    private LocationService _locService;
    public LocationController(LocationService locService)
    {
        _locService = locService;
    }
    [HttpGet("GetRegion")]
    public async Task<Response<List<Location>>> GetLocation()=> await _locService.GetLocation();

      [HttpPost("Insert")]
        public async Task<Response<int>> InsertLocation(Location Location)
        {
            return await _locService.InsertLocation(Location);
        }

          [HttpPut("Update")]
        public async Task <Response<int>> UpdateLocation(Location Location)
        {
            return await _locService.UpdateLocation(Location);
        }
           [HttpDelete]
         public async Task<Response<string>> DeleteLocation(int id)
        {
            return await _locService. DeleteLocation(id);
        }

}
    