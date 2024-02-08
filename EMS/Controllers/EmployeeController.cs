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
            return View(employeeDetails);
        }

        [HttpPost]
        public ActionResult EmployeeDetails(string employeeCodeSearch, string employeeNameSearch, string mailIDSearch, string contactNumberSearch, string genderSearch)
        {
            EmployeeDetailsBL employeeDetailsBL = new EmployeeDetailsBL();
            List<EmployeeDetails> employeeDetails = employeeDetailsBL.DisplayEmployeeDetails(); 
            if (employeeDetails != null)
            {
                employeeDetails = employeeDetails.Where(data =>
                    (string.IsNullOrEmpty(contactNumberSearch) || data.ContactNumber != null && data.ContactNumber.IndexOf(contactNumberSearch, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrEmpty(employeeCodeSearch) || data.EmployeeCode != null && data.EmployeeCode.IndexOf(employeeCodeSearch, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrEmpty(employeeNameSearch) || data.EmployeeName != null && data.EmployeeName.IndexOf(employeeNameSearch, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrEmpty(mailIDSearch) || data.MailID != null && data.MailID.IndexOf(mailIDSearch, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrEmpty(genderSearch) || data.Gender != null && data.Gender.IndexOf(genderSearch, StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();
            }
            if (employeeDetails != null && employeeDetails.Count > 0)
            {
                return PartialView("_partial", employeeDetails);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}