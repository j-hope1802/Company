
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos;


public class Employee
{
    public int EmployeeId { get; set; }
    public string   FirstName{ get; set; }
    public string LastName{get;set;}
    public string   Email{get;set;}
    public int PhoneNumber{get;set;}
    public int DepartmentId{get;set;}

public int Manager_id {get;set;}
public int Comission {get;set;}
public int Sallary {get;set;}
public DateTime HireDate;
public IFormFile Photo { get; set; }

}
public  class GetEmployee
{
     public int EmployeeId { get; set; }
    public string   FirstName{ get; set; }
    public string LastName{get;set;}
    public string   Email{get;set;}
    public int PhoneNumber{get;set;}
    public int DepartmentId{get;set;}

public int Manager_id {get;set;}
public int Comission {get;set;}
public int Sallary {get;set;}
public DateTime HireDate;
     public IFormFile ProfileImage { get; set; }

}