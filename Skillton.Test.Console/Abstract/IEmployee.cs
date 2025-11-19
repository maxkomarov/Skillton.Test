using System;

namespace Skillton.Test.Console_Net48.Abstract
{
    internal interface IEmployee
    {
        int EmployeeId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        DateTime DateOfBirth { get; set; }
        decimal Salary { get; set; }

        //Ну, не совсем он тут на месте....
        string ToConsoleString(char divider = '|', bool addHorizontalLine = false);
    }
}
