using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJsCodeFirst.ViewModels
{
    public class VmEmployee
    {
        public int Emp_Id { get; set; }
        public string Emp_Name { get; set; }
        public int? Desi_Id { get; set; }
        public int Emp_Age { get; set; }
        public DateTime Join_Date { get; set; }
        public string Gender { get; set; }
        public string PicPath { get; set; }
        public string Desi_Name { get; set; }
        public virtual VmDesignation Designation { get; set; }
    }
}