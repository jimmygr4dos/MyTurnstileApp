namespace MyTurnstileApp.Application.Interfaces
{
    public interface ILoggerService
    {
        /// <summary>
        /// Permite el registro de información en el archivo log.
        /// </summary>
        /// <param name="message">El mensaje a registrar.</param>
        /// <returns>Retorna una tarea asíncrona.</returns>
        Task Information(string message);
        /// <summary>
        /// Permite el registro de errores en el archivo log.
        /// </summary>
        /// <param name="message">El mensaje a registrar.</param>
        /// <returns>Retorna una tarea asíncrona.</returns>
        Task Error(string message);
    }
}
