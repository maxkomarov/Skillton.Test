using System;

namespace Skillton.Test.Console_Net48.Abstract
{
    internal interface IValidationService
    {

        /// <summary>
        /// Параметры валидации модели
        /// </summary>
        /// <value>
        /// The validation parameters.
        /// </value>
        IEmployeeValidationParams ValidationParams { get; }

        /// <summary>
        /// Валидация сущности
        /// </summary>
        /// <param name="employee">The employee.</param>
        void Validate(IEmployee employee);

        /// <summary>
        /// Проверка имени на длину
        /// </summary>
        /// <param name="value">The value.</param>
        void CheckFirstName(string value);

        /// <summary>
        /// Проверка фамилии на длину
        /// </summary>
        /// <param name="value">The value.</param>
        void CheckLastName(string value);

        /// <summary>
        /// Проверка поля на соответствие маске
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fieldName">Name of the field.</param>
        void CheckNameMaskValidity(string value, string fieldName);

        // <summary>
        /// Проверка поля на соответствие маске
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fieldName">Name of the field.</param>
        void CheckEmailMaskValidity(string value, string fieldName);

        /// <summary>
        /// Проверка даты рождения на соответствие диапазону
        /// </summary>
        /// <param name="value">The value.</param>
        void CheckDateOfBirthVilidity(DateTime value);

        /// <summary>
        /// Проверка суммы з/п на соответствие диапазону
        /// </summary>
        /// <param name="value">The value.</param>
        void CheckSalaryValidity(decimal value);
    }
}
