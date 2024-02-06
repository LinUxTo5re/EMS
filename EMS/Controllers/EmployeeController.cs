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
        /// <summary>
        /// Get api call for index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// post api call to store the employee details
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(EmployeeDetails employeeDetails)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    EmployeeDetailsBL employeeDetailsBL = new EmployeeDetailsBL();
                    if (employeeDetailsBL.AddEmployeeDetails(employeeDetails))
                    {
                        TempData["IsError"] = false;
                        TempData["Message"] = $"{employeeDetails.EmployeeName.ToUpper()} added successfully";
                    }
                    else
                    {
                        TempData["IsError"] = true;
                        TempData["Message"] = "Please fill out the mandatory fields or check the code";
                    }
                    return Json(new
                    {
                        IsError = TempData["IsError"],
                        Message = TempData["Message"]
                    });
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EmployeeDetails()
        {
            EmployeeDetailsBL employeeDetailsBL = new EmployeeDetailsBL();
            List<EmployeeDetails> employeeDetails = employeeDetailsBL.DisplayEmployeeDetails();
            employeeDetails = null;
            return View(employeeDetails);
        }
    }
}