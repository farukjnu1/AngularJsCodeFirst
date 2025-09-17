using AngularJsCodeFirst.Models;
using AngularJsCodeFirst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularJsCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Get_AllEmployee()
        {
            var listEmployee = new List<VmEmployee>();
            using (EmployeeContextDB _ctx = new EmployeeContextDB())
            {
                listEmployee = (from e in _ctx.Employees
                            join d in _ctx.Designations on e.Desi_Id equals d.Desi_Id
                            where e.Desi_Id == d.Desi_Id
                            select new VmEmployee()
                            {
                                Emp_Id = e.Emp_Id,
                                Emp_Age = e.Emp_Age,
                                Emp_Name = e.Emp_Name,
                                Gender = e.Gender,
                                Join_Date = e.Join_Date,
                                Desi_Id = e.Desi_Id,
                                Desi_Name = d.Desi_Name,
                                PicPath = e.PicPath
                            }).ToList();
            }
            return Json(listEmployee, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveEmp(VmEmployee employee)
        {
            var files = Request.Files;
            HttpPostedFileBase hpfb = null;
            if (Request.Files.Count > 0)
            {
                hpfb = Request.Files[0];
            }
            object result = null; string message = ""; bool resstate = false;
            if (employee != null)
            {
                //employee.Emp_Id = Convert.ToInt32(Request.Form["Emp_Id"]);
                //employee.Emp_Name = Request.Form["Emp_Name"];
                //employee.Emp_Age = Convert.ToInt32(Request.Form["Emp_Age"]);
                //employee.Gender = Request.Form["Gender"];
                //employee.Join_Date = Request.Form["Join_Date"];
                //employee.Desi_Id = Convert.ToInt32(Request.Form["Desi_Id"]);
                using (EmployeeContextDB _ctx = new EmployeeContextDB())
                {
                    try
                    {
                        var oEmployee = (from x in _ctx.Employees where x.Emp_Id == employee.Emp_Id select x).FirstOrDefault();
                        if (oEmployee == null)
                        {
                            #region insert
                            if (hpfb != null)
                            {
                                employee.PicPath = SaveFile(hpfb);
                            }
                            oEmployee = new Employee();
                            oEmployee.Desi_Id = employee.Desi_Id;
                            oEmployee.Emp_Age = employee.Emp_Age;
                            oEmployee.Emp_Name = employee.Emp_Name;
                            oEmployee.Gender = employee.Gender;
                            oEmployee.Join_Date = employee.Join_Date;
                            oEmployee.PicPath = employee.PicPath;
                            _ctx.Employees.Add(oEmployee);
                            message = "Employee Added Successfully.";
                            #endregion
                        }
                        else
                        {
                            #region update
                            if (hpfb != null)
                            {
                                if (!string.IsNullOrEmpty(oEmployee.PicPath))
                                {
                                    DeleteFile(oEmployee.PicPath);
                                }
                                employee.PicPath = SaveFile(hpfb);
                            }
                            oEmployee.Desi_Id = employee.Desi_Id;
                            oEmployee.Emp_Age = employee.Emp_Age;
                            oEmployee.Emp_Name = employee.Emp_Name;
                            oEmployee.Gender = employee.Gender;
                            oEmployee.Join_Date = employee.Join_Date;
                            oEmployee.PicPath = employee.PicPath;
                            message = "Employee Updated Successfully.";
                            #endregion
                        }
                        _ctx.SaveChanges();
                        resstate = true;
                    }
                    catch(Exception ex) { message = ex.Message; }
                }
            }
            else
            {
                message = "Data not valid.";
            }
            result = new
            {
                message,
                resstate
            };
            return Json(result);
        }

        #region files
        string SaveFile(HttpPostedFileBase hpfb)
        {
            var strTiem = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var newFileName = strTiem + "_" + hpfb.FileName;
            string physicalFilePath = Request.MapPath("~/img/" + newFileName);
            hpfb.SaveAs(physicalFilePath);
            return newFileName;
        }

        void DeleteFile(string fileName)
        {
            string physicalFilePath = Request.MapPath("~/img/" + fileName);
            if (System.IO.File.Exists(physicalFilePath))
            {
                System.IO.File.Delete(physicalFilePath);
            }
        }
        #endregion

        public string DeleteEmp(int id)
        {
            string message = "";
            using (EmployeeContextDB _ctx = new EmployeeContextDB())
            {
                var oEmployee = _ctx.Employees.Find(id);
                if (oEmployee != null)
                {
                    if (!string.IsNullOrEmpty(oEmployee.PicPath))
                    {
                        DeleteFile(oEmployee.PicPath);
                    }
                    _ctx.Employees.Remove(oEmployee);
                    _ctx.SaveChanges();
                    message = "Employee Deleted Successfully.";
                }
                else
                {
                    message = "Data not found.";
                }
            }
            return message;
        }
        public JsonResult GetDesignations()
        {
            var listDesignation = new List<VmDesignation>();
            using (EmployeeContextDB _ctx = new EmployeeContextDB())
            {
                listDesignation = (from x in _ctx.Designations
                                   select new VmDesignation
                                   {
                                       Desi_Id = x.Desi_Id,
                                       Desi_Name = x.Desi_Name
                                   }).ToList();
            }
            return Json(listDesignation, JsonRequestBehavior.AllowGet);
        }
    }
}