using Skillton.Test.Console_Net48.Abstract;

namespace Skillton.Test.Console_Net48.Controllers
{
    /// <summary>
    /// Форматтер для отображения IEmployee
    /// </summary>
    internal static class EmployeeFormatter
    {
        /// <summary>
        /// Вернуть строковое представление заголовка для таблицы IEmployee
        /// </summary>
        /// <param name="divider">The divider.</param>
        /// <returns></returns>
        public static string GetEmployeeTableHeader(char divider = '|')
        {
            return
                  "\t"
                + new string('-', 255)
                + "\r\n\t"
                + "ИД".PadRight(10)
                + divider
                + "Фамилия".PadRight(54)
                + divider
                + "Имя".PadRight(54)
                + divider
                + "E-mail".PadRight(104)
                + divider
                + "Дата рождения".PadRight(14)
                + divider
                + "Зарплата".PadRight(13)
                + divider
                + "\r\n\t"
                + new string('-', 255)
                .ToString();
        }

        /// <summary>
        /// Вернуть строковое представление IEmployee для таблицы
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <param name="divider">The divider.</param>
        /// <param name="addHorizontalLine">if set to <c>true</c> [add horizontal line].</param>
        /// <returns></returns>
        public static string ToRowString(
            IEmployee employee,
            char divider = '|',
            bool addHorizontalLine = false)
        {
            string res = $""
                + $"\t"
                + $"{employee.EmployeeId,-10}"
                + $"{divider}"
                + $"{employee.LastName,-54}"
                + $"{divider}"
                + $"{employee.FirstName,-54}"
                + $"{divider}"
                + $"{employee.Email,-104}"
                + $"{divider}"
                + $"{employee.DateOfBirth.ToShortDateString(),-14}"
                + $"{divider}"
                + $"{$"{employee.Salary:C2}",-13}"
                + $"{divider}";
            if (addHorizontalLine)
                res += ("\r\n\t" + new string('-', res.Length - 1));

            return res;
        }
    }
}
