using Skillton.Test.Console_Net48.Abstract;
using System;
using System.Collections.Generic;

namespace Skillton.Test.Console_Net48.Models
{
    internal class Employee : IEmployee
    {
        public Employee() { }

        public Employee(int employeeId,
                        string firstName,
                        string lastName,
                        string email,
                        DateTime dateOfBirth,
                        decimal salary)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            Salary = salary;
        }

        public Employee(IDictionary<string, object> rowData)
        {
            try
            {
                EmployeeId = rowData.ContainsKey("EmployeeID") ? (int)rowData["EmployeeID"] : -1;
                FirstName = rowData.ContainsKey("FirstName") ? rowData["FirstName"].ToString() : string.Empty;
                LastName = rowData.ContainsKey("LastName") ? rowData["LastName"].ToString() : string.Empty;
                Email = rowData.ContainsKey("Email") ? rowData["Email"].ToString() : string.Empty;
                DateOfBirth = rowData.ContainsKey("DateOfBirth") ? (DateTime)rowData["DateOfBirth"] : DateTime.MinValue;
                Salary = rowData.ContainsKey("Salary") ? (decimal)rowData["Salary"] : 0;
            }
            catch (Exception ex)
            {
                throw new System.Exception("Ошибка приведения атрибутов сущности. См.внутреннее исключение.", ex);
            }
        }

        //Это тоже, по идее, не свойство модели, нужен типа SampleBuilder<Employee> с рандомайзерами
        public static IList<IEmployee> GetSamples()
        {
            List<IEmployee> res = new List<IEmployee>
            {
                new Employee(0, "Иван", "Петров", "petrov@ivan.net", DateTime.Now.AddYears(-50), 1000),
                new Employee(0, "Петр", "Иванов", "ivanov@petr.net", DateTime.Now.AddYears(-40), 2000),
                new Employee(0, "Сидор", "Прохоров", "prokhorov@sidor.net", DateTime.Now.AddYears(-35), 3000)
            };

            return res;
        }

        

        

        #region Атрибуты модели
        //Вроде бы можно валидацию и в сеттеры совать, но это будет жесткая привязка сущности
        //к экземпляру валидатора - тоже кривовато, не по фэншую
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }

        #endregion

    }
}
