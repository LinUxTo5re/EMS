using EmployeeBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Globalization;

namespace EmployeeBusinessLayer.BusinessLayer
{
    public class EmployeeDetailsBL
    {
        /// <summary>
        /// Store employee details in DB using stored procedure
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <returns></returns>
        public bool AddEmployeeDetails(EmployeeDetails employeeDetails)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Business_Layer_DB_Connection"].ConnectionString;
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("spAddEmployeeDetails", con) { CommandType = CommandType.StoredProcedure })
            {
                try
                {
                    cmd.Parameters.Add(new SqlParameter("@EmployeeName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(employeeDetails.EmployeeName)));
                    cmd.Parameters.Add(new SqlParameter("@MailId", employeeDetails.MailID));
                    cmd.Parameters.Add(new SqlParameter("@EmployeeCode", int.TryParse(employeeDetails.EmployeeCode, out int employeeCode) ? (object)employeeCode : DBNull.Value));
                    if (employeeDetails.ContactNumber is null) // if null, store empty string
                        employeeDetails.ContactNumber = string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@MobileNumber", employeeDetails.ContactNumber));
                    if (employeeDetails.Gender is null)
                        employeeDetails.Gender = string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@Gender", employeeDetails.Gender));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Fetch employee details from DB using stored procedure
        /// </summary>
        /// <returns></returns>
        public List<EmployeeDetails> DisplayEmployeeDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Business_Layer_DB_Connection"].ConnectionString;
            List<EmployeeDetails> employeesDetails = new List<EmployeeDetails>();
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("spEmployeeDetails", con) { CommandType = CommandType.StoredProcedure })
            {
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            EmployeeDetails employee = new EmployeeDetails
                            {
                                EmployeeID = (int)reader["EmployeeId"],
                                EmployeeName = (string)reader["EmployeeName"],
                                EmployeeCode = reader["EmployeeCode"].ToString(),
                                MailID = (string)reader["MailId"],
                                ContactNumber = (string)reader["MobileNumber"],
                                Gender = (string)reader["Gender"]
                            };
                            employeesDetails.Add(employee);
                        }
                        reader.Close();
                    }
                    catch (Exception)
                    {
                        return employeesDetails ?? null; // return details whichever available until exception thrown (assignment op)
                    }
                    return employeesDetails;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
