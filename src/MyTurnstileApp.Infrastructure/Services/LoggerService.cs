using Microsoft.Extensions.Configuration;
using MyTurnstileApp.Application.Interfaces;

namespace MyTurnstileApp.Infrastructure.Services
{
    public class LoggerService: ILoggerService
    {
        private readonly string _pathFile;

        public LoggerService(IConfiguration configuration) 
        {
            _pathFile = configuration["Logger:PathFile"];
        }

        public async Task Error(string message)
        {
            using StreamWriter file = new(_pathFile, true);
            await file.WriteLineAsync("--- Mensaje de Error ---"
                                    + "\n Fecha: " + DateTime.Now.ToLongDateString()
                                    + "\n Hora: " + DateTime.Now.ToString("hh:mm:ss").ToString()
                                    + "\n Mensaje: " + message
                                    + "\n --- Fin de mensaje de Error ---");
        }

        public async Task Information(string message)
        {
            using StreamWriter file = new(_pathFile, true);
            await file.WriteLineAsync("--- Mensaje de Información ---"
                                    + "\n Fecha: " + DateTime.Now.ToLongDateString()
                                    + "\n Hora: " + DateTime.Now.ToString("hh:mm:ss").ToString()
                                    + "\n Mensaje: " + message
                                    + "\n --- Fin de mensaje de Información ---");
        }
    }
}
