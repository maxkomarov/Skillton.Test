using Skillton.Test.Console_Net48.Abstract;
using System;

namespace Skillton.Test.Console_Net48.Models
{
    internal class EmployeeValidationParams : IEmployeeValidationParams
    {
        /// <summary>
        /// Конструктор с загрузкой параметров по умолчанию
        /// Initializes a new instance of the <see cref="EmployeeValidationParams"/> class.
        /// </summary>
        public EmployeeValidationParams() { Load(); }

        #region Атрибуты

        /// <summary>
        /// Мин.длина поля имени
        /// </summary>
        /// <value>
        /// The first length of the name maximum.
        /// </value>
        public int FirstNameMinLength { get; set; }

        /// <summary>
        /// Мин.длина поля фамилии
        /// </summary>
        /// <value>
        /// The last length of the name maximum.
        /// </value>
        public int LastNameMinLength { get; set; }

        /// <summary>
        /// Макс.длина поля имени
        /// </summary>
        /// <value>
        /// The first length of the name maximum.
        /// </value>
        public int FirstNameMaxLength {get; set;}

        /// <summary>
        /// Макс.длина поля фамилии
        /// </summary>
        /// <value>
        /// The last length of the name maximum.
        /// </value>
        public int LastNameMaxLength {get; set;}

        /// <summary>
        /// Минимальное значение даты рождения
        /// </summary>
        /// <value>
        /// The date of birth minimum value.
        /// </value>
        public DateTime DateOfBirthMinValue {get; set;}

        /// <summary>
        /// Максимальное значение даты рождения
        /// </summary>
        /// <value>
        /// The date of birth maximum value.
        /// </value>
        public DateTime DateOfBirthMaxValue {get; set;}

        /// <summary>
        /// Маска допустимых символов в полях имени
        /// </summary>
        /// <value>
        /// The name value acceptable chars.
        /// </value>
        public string NameValueValidationMask {get; set;}

        /// <summary>
        /// Маска допустимых символов в поле email
        /// </summary>
        /// <value>
        /// The name value acceptable chars.
        /// </value>
        public string EmailValueValidationMask {get; set;}

        /// <summary>
        /// Минимальное значение зряплаты
        /// </summary>
        /// <value>
        /// The salary minimum value.
        /// </value>
        public decimal SalaryMinValue {get; set;}

        /// <summary>
        /// максимальное значение зряплаты
        /// </summary>
        /// <value>
        /// The salary minimum value.
        /// </value>
        public decimal SalaryMaxValue {get; set;}

        
        #endregion

        #region IConfigParams имплементация

        public void Load()
        {
            FirstNameMinLength = Properties.Settings.Default.ValidatorFirstNameMinLength;
            LastNameMinLength = Properties.Settings.Default.ValidatorLastNameMinLength;
            FirstNameMaxLength = Properties.Settings.Default.ValidatorFirstNameMaxLength;
            LastNameMaxLength = Properties.Settings.Default.ValidatorLastNameMaxLength;
            NameValueValidationMask = Properties.Settings.Default.ValidatorNameValueValidationMask;
            EmailValueValidationMask = Properties.Settings.Default.ValidatorEmailValueValidationMask;
            DateOfBirthMinValue = Properties.Settings.Default.ValidatorDateOfBirthMinValue;
            DateOfBirthMaxValue = Properties.Settings.Default.ValidatorDateOfBirthMaxValue;
            SalaryMinValue = Properties.Settings.Default.ValidatorSalaryMinValue;
            SalaryMaxValue = Properties.Settings.Default.ValidatorSalaryMaxValue;
        }

        public void Save()
        {
            Properties.Settings.Default.ValidatorFirstNameMinLength = FirstNameMinLength;
            Properties.Settings.Default.ValidatorLastNameMinLength = LastNameMinLength;
            Properties.Settings.Default.ValidatorFirstNameMaxLength = FirstNameMaxLength;
            Properties.Settings.Default.ValidatorLastNameMaxLength = LastNameMaxLength;
            Properties.Settings.Default.ValidatorNameValueValidationMask = NameValueValidationMask;
            Properties.Settings.Default.ValidatorEmailValueValidationMask = EmailValueValidationMask;
            Properties.Settings.Default.ValidatorDateOfBirthMinValue = DateOfBirthMinValue;
            Properties.Settings.Default.ValidatorDateOfBirthMaxValue = DateOfBirthMaxValue;
            Properties.Settings.Default.ValidatorSalaryMinValue = SalaryMinValue;
            Properties.Settings.Default.ValidatorSalaryMaxValue = SalaryMaxValue;
        }

        #endregion
    }
}
