using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

using Npgsql;

namespace Infrastructure.Services;

public class CountryService
{
    private readonly DapperContext _context;
    private readonly IWebHostEnvironment _hosting;

    public CountryService(DapperContext context,IWebHostEnvironment hosting)
    {
        _context = context;
        _hosting = hosting;
    }

     public async Task<Response<List<Country>>> GetCountry()
        {
            using (var conn = _context.CreateConnection())

            {
                var con = await conn.QueryAsync<Country>("select c.country_id as CountryId, r.region_id as RegionId, c.country_name as CountryName from countries as c Join regions as r on r.region_id = c.region_id;");
                return new Response<List<Country>>(con.ToList());
            }
        }

        public async Task <Response<int>> InsertCountry(Country country)
        {


 using (var conn = _context.CreateConnection())

            {
                 var sql =
                    $"insert into countries (country_name,region_id) VALUES " +
                    $"('{country.CountryName}' ," +
                    $"'{country.RegionId}' )" ;
                   
                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);

        }
        
}
   public async  Task<Response<int>> UpdateCountry(Country country)
        {
 using (var conn = _context.CreateConnection())
            {
                var sql = 
                    $"UPDATE countries SET " +
                    $"country_name = '{country.CountryName}', " +
                    $"region_id= '{country.RegionId}' "+ 
                    $"WHERE country_id = {country.CountryId}";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
         public async Task<Response<string>> DeleteCountry(int id)
        {
            using (var conn = _context.CreateConnection())
            {
               var response = await conn.ExecuteAsync($"delete from countries where country_id= {id}");
            if(response > 0)
                return new Response<string>("Country deleted successfully");
        
            return new Response<string>(HttpStatusCode.BadRequest, "country not found");
            }
        }
}
