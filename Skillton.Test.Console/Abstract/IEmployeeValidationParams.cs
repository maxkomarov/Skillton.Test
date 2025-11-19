using System;

namespace Skillton.Test.Console_Net48.Abstract
{
    /// <summary>
    /// Интерфейс параметров валидации модели IEmployee
    /// </summary>
    internal interface IEmployeeValidationParams : IConfigParams
    {        

        /// <summary>
        /// Мин.длина поля имени
        /// </summary>
        /// <value>
        /// The first length of the name maximum.
        /// </value>
        int FirstNameMinLength { get; set; }

        /// <summary>
        /// Мин.длина поля фамилии
        /// </summary>
        /// <value>
        /// The last length of the name maximum.
        /// </value>
        int LastNameMinLength { get; set; }

        /// <summary>
        /// Макс.длина поля имени
        /// </summary>
        /// <value>
        /// The first length of the name maximum.
        /// </value>
        int FirstNameMaxLength { get; set; }

        /// <summary>
        /// Макс.длина поля фамилии
        /// </summary>
        /// <value>
        /// The last length of the name maximum.
        /// </value>
        int LastNameMaxLength { get; set; }

        /// <summary>
        /// Минимальное значение даты рождения
        /// </summary>
        /// <value>
        /// The date of birth minimum value.
        /// </value>
        DateTime DateOfBirthMinValue { get; set; }

        /// <summary>
        /// Максимальное значение даты рождения
        /// </summary>
        /// <value>
        /// The date of birth maximum value.
        /// </value>
        DateTime DateOfBirthMaxValue { get; set; }

        /// <summary>
        /// Маска допустимых символов в полях имени
        /// </summary>
        /// <value>
        /// The name value acceptable chars.
        /// </value>
        string NameValueValidationMask { get; set; }

        /// <summary>
        /// Маска допустимых символов в поле email
        /// </summary>
        /// <value>
        /// The name value acceptable chars.
        /// </value>
        string EmailValueValidationMask { get; set; }

        /// <summary>
        /// Минимальное значение зряплаты
        /// </summary>
        /// <value>
        /// The salary minimum value.
        /// </value>
        decimal SalaryMinValue { get; set; }

        /// <summary>
        /// максимальное значение зряплаты
        /// </summary>
        /// <value>
        /// The salary minimum value.
        /// </value>
        decimal SalaryMaxValue { get; set; }
    }
}
