using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols;
using System.Data;
using System.Reflection;

namespace EmployeeManegement
{
    public class EmployeeRepo
    {
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Employee_Payroll;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

        public void GetAllEmployee()
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"SELECT EmployeeID,EmployeeName,PhoneNumber,Address,Department,Gender,BasicPay,Deductions,TaxeblePay,Tax,NetPay,StartDate,City,Country from employee_payroll;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employeeModel.EmployeeID = dr.GetInt32(0);
                            employeeModel.EmployeeName = dr.GetString(1);
                            employeeModel.PhoneNumber = dr.GetString(2);
                            employeeModel.Address = dr.GetString(3);
                            employeeModel.Department = dr.GetString(4);
                            employeeModel.Gender = Convert.ToChar(dr.GetString(5));
                            employeeModel.BasicPay = dr.GetDouble(6);
                            employeeModel.Deductions = dr.GetDouble(7);
                            employeeModel.TaxeblePay = dr.GetDouble(8);
                            employeeModel.Tax = dr.GetDouble(9);
                            employeeModel.NetPay = dr.GetDouble(10);
                            employeeModel.StartDate = dr.GetDateTime(11);
                            employeeModel.City = dr.GetString(12);
                            employeeModel.Country = dr.GetString(13);

                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{5},{6},{7},{8},{9},{10},{11},{12},{13}", employeeModel.EmployeeID, employeeModel.EmployeeName, employeeModel.PhoneNumber, employeeModel.Address, employeeModel.Department, employeeModel.Gender,
                                employeeModel.BasicPay, employeeModel.Deductions, employeeModel.TaxeblePay, employeeModel.Tax, employeeModel.NetPay, employeeModel.StartDate, employeeModel.City, employeeModel.Country);
                        }
                    }
                    else    
                    {
                        Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("SpAddEmployeeDetails", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxeblePay", model.TaxeblePay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@Country", model.Country);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        public int UpdateEmployeeSalary(SalaryUpdate model)
        {
            SqlConnection salaryConnection = connection;
            int salary = 0;
            try
            {
                using (salaryConnection)
                {
                    EmployeeModel displaymodel = new EmployeeModel();
                    SqlCommand command = new SqlCommand("SpAddEmployeeDetails", salaryConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxeblePay", model.TaxeblePay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@Country", model.Country);
                    salaryConnection.Open();
                   
                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            displaymodel.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                            displaymodel.EmployeeName = dr["EmployeeName"].ToString();
                            displaymodel.PhoneNumber = dr["PhoneNumber"].ToString();
                            displaymodel.Address = dr["Address"].ToString();
                            displaymodel.Department = dr["Deparatment"].ToString();
                            displaymodel.BasicPay = Convert.ToInt32(dr["BasicPay"]);
                            displaymodel.Deductions = Convert.ToInt32(dr["Deductions"]);
                            displaymodel.TaxeblePay = Convert.ToInt32(dr["TaxeblePay"]);
                            displaymodel.Tax = Convert.ToInt32(dr["Tax"]);
                            displaymodel.NetPay = Convert.ToInt32(dr["NetPay"]);
                            displaymodel.City = dr["City"].ToString();
                            displaymodel.Country = dr["Country"].ToString();
                            Console.WriteLine("{0} - {1} - {2}", displaymodel.Address, displaymodel.Department, displaymodel.BasicPay, displaymodel.Deductions, displaymodel.TaxeblePay);
                            salary = (int)displaymodel.Deductions;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                salaryConnection.Close();
            }
            return salary;
        }
    }
}