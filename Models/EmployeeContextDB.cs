using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
//Install - Package EntityFramework enable-migrations
//add - migration InitialCreate  update-database
namespace AngularJsCodeFirst.Models
{
    public class EmployeeContextDB : DbContext
    {
        public EmployeeContextDB() : base("EmpDBContext")
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Designation> Designations { get; set; }
    }
}