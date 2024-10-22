using MyTurnstileApp.Domain.Entities;

namespace MyTurnstileApp.Application.Interfaces
{
    public interface IUserSubmissionService
    {
        Task<bool> ValidateTurnstileAsync(string token, string remoteIp);
        Task<UserSubmission> SubmitAsync(UserSubmission submission);
    }
}
