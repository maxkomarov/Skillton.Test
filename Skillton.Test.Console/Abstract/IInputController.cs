namespace Skillton.Test.Console_Net48.Abstract
{
    /// <summary>
    /// Интерфейс контроллера ввода
    /// </summary>
    internal interface IInputController
    {
        /// <summary>
        /// Валидатор ввода
        /// </summary>
        /// <value>
        /// The validation controller.
        /// </value>
        IValidationController ValidationController { get; }

        /// <summary>
        /// Запуск рабочего процесса
        /// </summary>
        void Run();
    }
}
