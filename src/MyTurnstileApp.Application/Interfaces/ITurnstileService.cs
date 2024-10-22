namespace MyTurnstileApp.Application.Interfaces
{
    public interface ITurnstileService
    {
        /// <summary>
        /// Valida el token de Turnstile recibido desde el frontend.
        /// </summary>
        /// <param name="token">El token de respuesta de Turnstile.</param>
        /// <param name="remoteIp">La dirección IP remota del usuario.</param>
        /// <returns>Retorna true si el token es válido; de lo contrario, false.</returns>
        Task<bool> ValidateAsync(string token, string remoteIp);
    }
}
