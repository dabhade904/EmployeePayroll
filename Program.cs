using System.Reflection.Metadata;

namespace EmployeeManegement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EmployeeRepo employeeRepo = new EmployeeRepo();
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeName = "kalmesh";
            employeeModel.PhoneNumber = "9309739906";
            employeeModel.Address = "mukhpath";
            employeeModel.Department = "cyber";
            employeeModel.Gender = 'M';
            employeeModel.BasicPay = 2000;
            employeeModel.Deductions = 3000;
            employeeModel.TaxeblePay = 5000;
            employeeModel.Tax = 1000;
            employeeModel.NetPay = 50000;
            employeeModel.City = "Aurangabad";
            employeeModel.Country = "India";

           // employeeRepo.AddEmployee(employeeModel);
            employeeRepo.GetAllEmployee();
        }
    }
}