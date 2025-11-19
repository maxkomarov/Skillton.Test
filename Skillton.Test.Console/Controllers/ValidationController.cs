using Skillton.Test.Console_Net48.Abstract;
using System;
using System.Text.RegularExpressions;

namespace Skillton.Test.Console_Net48.Controllers
{
    internal class ValidationController : IValidationController
    {
        #region Константы

        private const string STRING_LENGTH_OUT_OF_RANGE = "Длина строки вне допустимых пределов (%min% < %value% <= %maх%)";
        private const string SALARY_VALUE_OUT_OF_RANGE = "Размер зарплаты вне допустимых пределов (%min% < %value% <= %maх%)";
        private const string DATE_OF_BIRTH_VALUE_OUT_OF_RANGE = "Дата рождения вне допустимых пределов (%min% < %value% <= %max%)";
        private const string FORMAT_INVALID = "Предоставленное значение не удовлетворяет требованиям формата поля [%field%] - [%value%], формат:[%format%]";
        public const string NOTHING_TO_VALIDATE = "Не предоставлен объект для валидации";
        public const string NO_PARAMS_FOR_VALIDATE = "Не предоставлены параметры валидации";

        #endregion

        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationController"/> class.
        /// Параметры валидации обязательны
        /// </summary>
        /// <param name="validationParams">The validation parameters.</param>
        public ValidationController(IEmployeeValidationParams validationParams) 
        {
            ValidationParams = validationParams ?? throw new ArgumentNullException(NO_PARAMS_FOR_VALIDATE, nameof(validationParams));
        }

        #endregion

        #region Props

        /// <summary>
        /// Параметры валидации модели
        /// </summary>
        /// <value>
        /// The validation parameters.
        /// </value>
        public IEmployeeValidationParams ValidationParams { get; private set; }

        #endregion

        #region Методы валидации

        /// <summary>
        /// Валидация сущности
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Validate(IEmployee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(NOTHING_TO_VALIDATE); 
            
            try
            {
                CheckFirstName(employee.FirstName);
                CheckLastName(employee.LastName);
                CheckNameMaskValidity(employee.FirstName, "FirstName");
                CheckNameMaskValidity(employee.LastName, "LastName");
                CheckEmailMaskValidity(employee.Email, "Email");
                CheckDateOfBirthVilidity(employee.DateOfBirth);
                CheckSalaryValidity(employee.Salary);
            }
            catch (Exception e) 
            {
                throw new Exception("Введенные данные некорректны!", e);
            }
        }

        /// <summary>
        /// Проверка имени на длину
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">value</exception>
        public void CheckFirstName(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException($"{NOTHING_TO_VALIDATE}: [FirstName]");

            if (value.Length > ValidationParams.FirstNameMaxLength
                || value.Length <= ValidationParams.FirstNameMinLength)
                throw new ArgumentOutOfRangeException(
                    STRING_LENGTH_OUT_OF_RANGE
                        .Replace("%min%", ValidationParams.FirstNameMinLength.ToString())
                        .Replace("%value%", value)
                        .Replace("%maх%", ValidationParams.FirstNameMaxLength.ToString()),
                    nameof(value));
        }

        /// <summary>
        /// Проверка фамилии на длину
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">value</exception>
        public void CheckLastName(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException($"{NOTHING_TO_VALIDATE}: [LastName]");

            if (value.Length > ValidationParams.LastNameMaxLength
                || value.Length <= ValidationParams.LastNameMinLength)
                throw new ArgumentOutOfRangeException(
                    STRING_LENGTH_OUT_OF_RANGE
                        .Replace("%min%", ValidationParams.FirstNameMinLength.ToString())
                        .Replace("%value%", value)
                        .Replace("%maх%", ValidationParams.LastNameMaxLength.ToString()),
                    nameof(value));
        }


        /// <summary>
        /// Проверка поля на соответствие маске
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <exception cref="System.ArgumentException">value</exception>
        public void CheckNameMaskValidity(string value, string fieldName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException($"{NOTHING_TO_VALIDATE}: [{fieldName}]");

            if (!Regex.IsMatch(value, ValidationParams.NameValueValidationMask))
                throw new ArgumentException(
                    FORMAT_INVALID
                    .Replace("%field%", fieldName)
                    .Replace("%value%", value)
                    .Replace("%format%", "A-Za-zА-Яа-я")
                    , nameof(value));

        }

        // <summary>
        /// Проверка email на соответствие маске
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <exception cref="System.ArgumentException">value</exception>
        public void CheckEmailMaskValidity(string value, string fieldName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException($"{NOTHING_TO_VALIDATE}: [{fieldName}]");

            if (!Regex.IsMatch(value, ValidationParams.EmailValueValidationMask))
                throw new ArgumentException(
                    FORMAT_INVALID
                    .Replace("%field%", fieldName)
                    .Replace("%value%", value)
                    .Replace("%format%", "a@b.c")
                    , nameof(value));

        }

        /// <summary>
        /// Проверка даты рождения на соответствие диапазону
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">value</exception>
        public void CheckDateOfBirthVilidity(DateTime value)
        {
            if (value > ValidationParams.DateOfBirthMaxValue
                || value <= ValidationParams.DateOfBirthMinValue)
                throw new ArgumentOutOfRangeException(
                    DATE_OF_BIRTH_VALUE_OUT_OF_RANGE
                        .Replace("%min%", ValidationParams.DateOfBirthMinValue.ToShortDateString())
                        .Replace("%value%", value.ToShortDateString())
                        .Replace("%max%", ValidationParams.DateOfBirthMaxValue.ToShortDateString()),
                    nameof(value));
        }

        /// <summary>
        /// Проверка суммы з/п на соответствие диапазону
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">value</exception>
        public void CheckSalaryValidity(decimal value)
        {
            if (value > ValidationParams.SalaryMaxValue
                || value <= ValidationParams.SalaryMinValue)
                throw new ArgumentOutOfRangeException(
                    SALARY_VALUE_OUT_OF_RANGE
                        .Replace("%min%", ValidationParams.SalaryMinValue.ToString("G"))
                        .Replace("%value%", value.ToString("G"))
                        .Replace("%max%", ValidationParams.SalaryMaxValue.ToString("G")),
                    nameof(value));
        }

        #endregion
    }
}
