using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngularJsCodeFirst.Models
{
    public class Designation
    {
        [Key]
        public int Desi_Id { get; set; }
        public string Desi_Name { get; set; }
        public virtual IList<Employee> Employees { get; set; }
    }
}