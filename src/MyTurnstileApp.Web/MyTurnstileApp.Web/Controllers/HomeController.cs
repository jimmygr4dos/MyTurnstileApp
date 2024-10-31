using Microsoft.AspNetCore.Mvc;
using MyTurnstileApp.Application.Interfaces;
using MyTurnstileApp.Domain.Entities;
using MyTurnstileApp.Web.Models;

namespace MyTurnstileApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserSubmissionService _submissionService;
        private readonly IConfiguration _configuration;

        public HomeController(IUserSubmissionService submissionService, IConfiguration configuration)
        {
            _submissionService = submissionService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //ViewData["SiteKey"] = _configuration["Turnstile:SiteKey"];
            ViewData["SiteKey"] = _configuration["Google:SiteKey"];

            //string remoteIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

            string forwarded = HttpContext.Request.Headers["X-Forwarded-For"].ToString() ?? "";
            string remoteIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

            ViewData["Forwarded"] = forwarded;
            ViewData["RemoteIP"] = remoteIP;

            return View(new SubmissionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Submit(SubmissionViewModel model)
        {
            // Asigna el token de Turnstile desde el formulario
            //model.TurnstileResponse = Request.Form["cf-turnstile-response"];

            // Asigna el token de Google desde el formulario
            //model.RecaptchaToken = Request.Form["g-recaptcha-response"];

            if (!ModelState.IsValid)
                return View("Index", model);

            string remoteIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

            //var isValid = await _submissionService.ValidateTurnstileAsync(model.TurnstileResponse, remoteIP);
            var isValid = await _submissionService.ValidateTurnstileAsync(model.RecaptchaToken, remoteIP);

            if (!isValid)
            {
                //ModelState.AddModelError(string.Empty, "Token de Turnstile inválido.");
                ModelState.AddModelError(string.Empty, "Token de Google inválido.");
                return View("Index", model);
            }

            var submission = new UserSubmission
            {
                UserName = model.UserName,
                Email = model.Email,
                TurnstileResponse = model.TurnstileResponse,
                SubmittedAt = DateTime.UtcNow
            };

            await _submissionService.SubmitAsync(submission);

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
