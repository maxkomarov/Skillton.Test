using Skillton.Test.Console_Net48.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

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
            List<IEmployee> res = new List<IEmployee>();

            res.Add(new Employee(0, "Иван", "Петров", "petrov@ivan.net", DateTime.Now.AddYears(-50), 1000));
            res.Add(new Employee(0, "Петр", "Иванов", "ivanov@petr.net", DateTime.Now.AddYears(-40), 2000));
            res.Add(new Employee(0, "Сидор", "Прохоров", "prokhorov@sidor.net", DateTime.Now.AddYears(-35), 3000));

            return res;
        }

        //Этот метод не на месте, надо бы выносить в отдельный форматтер
        public string ToConsoleString(char divider = '|',
                                      bool addHorizontalLine = false)
        {
            string res = new StringBuilder()
                .Append('\t')
                .Append(EmployeeId.ToString().PadRight(10))
                .Append(divider)
                .Append(LastName.PadRight(54))
                .Append(divider)
                .Append(FirstName.PadRight(54))
                .Append(divider)
                .Append(Email.PadRight(104))
                .Append(divider)
                .Append($"{DateOfBirth:d}".PadRight(14))
                .Append(divider)
                .Append($"{Salary:C2}".PadRight(13))
                .Append(divider)
                .ToString();
            if (addHorizontalLine)
                res += ("\r\n\t" + new string('-', res.Length-1));

            return res;
        }

        //Этот метод не на месте, надо бы выносить в отдельный форматтер
        public static string GetTableHeader(char divider = '|')
        {
            return new StringBuilder()
                .Append('\t')
                .Append(new string('-', 255))
                .Append("\r\n\t")
                .Append("ИД".PadRight(10))
                .Append(divider)
                .Append("Фамилия".PadRight(54))
                .Append(divider)
                .Append("Имя".PadRight(54))
                .Append(divider)
                .Append("E-mail".PadRight(104))
                .Append(divider)
                .Append("Дата рождения".PadRight(14))
                .Append(divider)
                .Append("Зарплата".PadRight(13))
                .Append(divider)
                .Append("\r\n\t")
                .Append(new string('-', 255))
                .ToString();    
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
