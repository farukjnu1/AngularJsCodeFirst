using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJsCodeFirst.ViewModels
{
    public class VmDesignation
    {
        public int Desi_Id { get; set; }
        public string Desi_Name { get; set; }
        public virtual IList<VmEmployee> Employees { get; set; }
    }
}