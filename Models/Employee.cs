using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngularJsCodeFirst.Models
{
    public class Employee
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Emp_Id { get; set; }
        public string Emp_Name { get; set; }
        [ForeignKey("Designation")]
        public int? Desi_Id { get; set; }
        public int Emp_Age { get; set; }
        public DateTime Join_Date { get; set; }
        public string Gender { get; set; }
        public string PicPath { get; set; }
        public virtual Designation Designation { get; set; }
    }
}