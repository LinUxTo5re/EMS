using EmployeeBusinessLayer.BusinessLayer;
using EmployeeBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmployeeDetails employeeDetails)
        {
            if(ModelState.IsValid)
            {
                EmployeeDetailsBL employeeDetailsBL = new EmployeeDetailsBL();
                employeeDetailsBL.AddEmployeeDetails(employeeDetails);
                //{
                //    ViewBag.IsError = false;
                //    ViewBag.Message = $"{employeeDetails.EmployeeName} added successfully";
                //}
                //else
                //{
                //    ViewBag.IsError = true;
                //    ViewBag.Message = "Error raised while adding, please check code";
                //}
            }
            return View();
        }
    }
}