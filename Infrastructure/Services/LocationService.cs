using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;

using Npgsql;

namespace Infrastructure.Services;

public class LocationService
{
    private readonly DapperContext _context;
    private readonly IWebHostEnvironment _hosting;

    public LocationService(DapperContext context,IWebHostEnvironment hosting)
    {
        _context = context;
        _hosting = hosting;
    }

     public async Task<Response<List<Location>>> GetLocation()
        {
            using (var conn = _context.CreateConnection())

            {
                var loc = await conn.QueryAsync<Location>("select l.location_id as LocationId, l.postal_code as PostalCode,l.city as City ,l.state_province as StateProvince,c.country_id as CountryId,l.street_address  as StreetAddreses from locations as l  Join countries as c on l.country_id = c.country_id;");
                return new Response<List<Location>>(loc.ToList());
            }
        }

        public async Task <Response<int>> InsertLocation(Location location)
        {


 using (var conn = _context.CreateConnection())

            {
                 var sql =
                    $"insert into locations (street_address,postal_code,city,state_province,country_id) VALUES " +
                    $"('{location.StreetAddreses}' ," +
                      $"'{location.PostalCode}' ," +
                        $"'{location.City}' ," +
                          $"'{location.StateProvince}' ," +
                         $"{location.CountryId} )" ;
                   
                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);

        }
        
}
   public async  Task<Response<int>> UpdateLocation(Location location)
        {
 using (var conn = _context.CreateConnection())
            {
                var sql = 
                    $"UPDATE countries SET " +
                    $"street_address = '{location.StreetAddreses}', " +
                     $"postal_code = '{location.PostalCode}', " +
                      $"city = '{location.City}', " +
                       $"state_province = '{location.StateProvince}', " +
                    $"country_id= '{location.CountryId}' "+ 
                    $"WHERE location_id = {location.LocationId}";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
         public async Task<Response<string>> DeleteLocation(int id)
        {
            using (var conn = _context.CreateConnection())
            {
               var response = await conn.ExecuteAsync($"delete from locations where location_id= {id}");
            if(response > 0)
                return new Response<string>("Location deleted successfully");
        
            return new Response<string>(HttpStatusCode.BadRequest, "Location not found");
            }
        }
}
