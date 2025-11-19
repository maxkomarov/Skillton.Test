namespace Skillton.Test.Console_Net48.Abstract
{
    /// <summary>
    /// Константы для запросов к БД
    /// </summary>
    internal static class QueryConstants
    {
        public const string UPDATE_CMD_TEXT = "UPDATE Employees SET FirstName=@FirstName, LastName=@LastName, Email=@Email, DateOfBirth=@DateOfBirth, Salary=@Salary WHERE EmployeeID=@EmployeeID";

        public const string INSERT_CMD_TEXT = "INSERT INTO Employees (FirstName, LastName, Email, DateOfBirth, Salary) VALUES (@firstName, @lastName, @email, @dateOfBirth, @salary)";

        public const string DELETE_CMD_TEXT = "DELETE Employees WHERE EmployeeID=@EmployeeID";

        public const string SELECT_ALL_CMD_TEXT = "SELECT * FROM Employees ORDER BY LastName, FirstName";

        public const string SELECT_BY_ID_CMD_TEXT = "SELECT * FROM Employees WHERE EmployeeID=@EmployeeID";

        public const string SELECT_AVG_SALARY_CMD_TEXT = "select avg(Salary) from Employees";

        public const string SELECT_SALARY_ABV_AVG_CMD_TEXT = "select count(*) from Employees where Salary > @avg";

    }
}
