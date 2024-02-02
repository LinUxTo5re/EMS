using EmployeeBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EmployeeBusinessLayer.BusinessLayer
{
    public class EmployeeDetailsBL
    {
        public bool AddEmployeeDetails(EmployeeDetails employeeDetails)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Business_Layer_DB_Connection"].ConnectionString;
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("spAddEmployeeDetails", con) { CommandType = CommandType.StoredProcedure })
            {
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@EmployeeName", employeeDetails.EmployeeName));
                    cmd.Parameters.Add(new SqlParameter("@MailId", employeeDetails.MailID));
                    cmd.Parameters.Add(new SqlParameter("@EmployeeCode", int.TryParse(employeeDetails.EmployeeCode, out int employeeCode) ? (object)employeeCode : DBNull.Value));
                    if (employeeDetails.ContactNumber is null)
                        employeeDetails.ContactNumber = string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@MobileNumber", employeeDetails.ContactNumber));
                    if (employeeDetails.Gender is null)
                        employeeDetails.Gender = string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Gender", employeeDetails.Gender));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }
    }
}
