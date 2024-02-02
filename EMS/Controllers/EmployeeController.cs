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
                if(employeeDetailsBL.AddEmployeeDetails(employeeDetails))
                {
                    TempData["IsError"] = false;
                    TempData["Message"] = $"{employeeDetails.EmployeeName.ToUpper()} added successfully";
                }
                else
                {
                    TempData["IsError"] = true;
                    TempData["Message"] = "Please fill the manadatory fields or check code";
                }
                return Json(new
                {
                    IsError = TempData["IsError"],
                    Message = TempData["Message"]
                });
            }
            return RedirectToAction("Index");
        }
    }
}