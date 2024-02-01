using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBusinessLayer.Models
{
    /// <summary>
    /// EmployeeDetails Table Properties
    /// </summary>
    public class EmployeeDetails
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string MailID { get; set; }
        public string ContactNumber { get; set; }
        public string EmployeeCode { get; set; }
    }
}
