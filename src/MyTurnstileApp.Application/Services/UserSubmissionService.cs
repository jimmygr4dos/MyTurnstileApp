using MyTurnstileApp.Application.Interfaces;
using MyTurnstileApp.Domain.Entities;

namespace MyTurnstileApp.Application.Services
{
    public class UserSubmissionService : IUserSubmissionService
    {
        private readonly ITurnstileService _turnstileService;

        public UserSubmissionService(ITurnstileService turnstileService)
        {
            _turnstileService = turnstileService;
        }

        public async Task<bool> ValidateTurnstileAsync(string token, string remoteIp)
        {
            return await _turnstileService.ValidateAsync(token, remoteIp);
        }

        public async Task<UserSubmission> SubmitAsync(UserSubmission submission)
        {
            // Aquí puedes implementar la lógica para almacenar la sumisión
            // Por ejemplo, usando un repositorio o una base de datos
            // Por simplicidad, simplemente devolvemos la sumisión con un Id ficticio

            submission.Id = new Random().Next(1, 1000);
            // Simular una operación asíncrona
            await Task.Delay(100);
            return submission;
        }
    }
}
