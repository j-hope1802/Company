using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController
{
    private CountryService _conService;
    public CountryController(CountryService conService)
    {
        _conService = conService;
    }
    [HttpGet("GetRegion")]
    public async Task<Response<List<Country>>> GetCountry()=> await _conService.GetCountry();

      [HttpPost("Insert")]
        public async Task<Response<int>> InsertCountry(Country country)
        {
            return await _conService.InsertCountry(country);
        }

          [HttpPut("Update")]
        public async Task <Response<int>> UpdateCountry(Country country)
        {
            return await _conService.UpdateCountry(country);
        }
           [HttpDelete]
         public async Task<Response<string>> DeleteCountry(int id)
        {
            return await _conService. DeleteCountry(id);
        }

}
    