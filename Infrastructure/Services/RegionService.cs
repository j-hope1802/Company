using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

using Npgsql;

namespace Infrastructure.Services;

public class RegionService
{
    private readonly DapperContext _context;
    private readonly IWebHostEnvironment _hosting;

    public RegionService(DapperContext context,IWebHostEnvironment hosting)
    {
        _context = context;
        _hosting = hosting;
    }

     public async Task<Response<List<Region>>> GetRegion()
        {
            using (var conn = _context.CreateConnection())

            {
                var reg = await conn.QueryAsync<Region>("SELECT region_name as RegionName,region_id as RegionId  from regions");
                return new Response<List<Region>>(reg.ToList());
            }
        }

        public async Task <Response<int>> InsertRegion(Region region)
        {


 using (var conn = _context.CreateConnection())

            {
                 var sql =
                    $"insert into regions (region_name) VALUES " +
                
                    $"('{region.RegionName}' )" ;
                   
                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);

        }
        
}
   public async  Task<Response<int>> UpdateRegion(Region region)
        {
 using (var conn = _context.CreateConnection())
            {
                var sql = 
                    $"UPDATE regions SET " +
            
                    $"region_name = '{region.RegionName}' "+ 
                    $"WHERE region_id = {region.RegionId}";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
         public async Task<Response<string>> DeleteRegion(int id)
        {
            using (var conn = _context.CreateConnection())
            {
               var response = await conn.ExecuteAsync($"delete from regions where region_id= {id}");
            if(response > 0)
                return new Response<string>("Region deleted successfully");
        
            return new Response<string>(HttpStatusCode.BadRequest, "Region not found");
            }
        }
}
