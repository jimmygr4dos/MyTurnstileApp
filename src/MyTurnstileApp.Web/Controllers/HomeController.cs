//// src/MyTurnstileApp.Web/Controllers/HomeController.cs
//using Microsoft.AspNetCore.Mvc;
//using MyTurnstileApp.Application.Interfaces;
//using MyTurnstileApp.Domain.Entities;
//using MyTurnstileApp.Web.Models;

//namespace MyTurnstileApp.Web.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly IUserSubmissionService _submissionService;
//        private readonly IConfiguration _configuration;

//        public HomeController(IUserSubmissionService submissionService, IConfiguration configuration)
//        {
//            _submissionService = submissionService;
//            _configuration = configuration;
//        }

//        [HttpGet]
//        public IActionResult Index()
//        {
//            ViewData["SiteKey"] = _configuration["Turnstile:SiteKey"];
//            return View(new SubmissionViewModel());
//        }

//        [HttpPost]
//        public async Task<IActionResult> Submit(SubmissionViewModel model)
//        {
//            ViewData["SiteKey"] = _configuration["Turnstile:SiteKey"];

//            if (!ModelState.IsValid)
//                return View("Index", model);

//            var isValid = await _submissionService.ValidateTurnstileAsync(model.TurnstileResponse, HttpContext.Connection.RemoteIpAddress?.ToString() ?? "");

//            if (!isValid)
//            {
//                ModelState.AddModelError(string.Empty, "Invalid Turnstile token.");
//                return View("Index", model);
//            }

//            var submission = new UserSubmission
//            {
//                UserName = model.UserName,
//                Email = model.Email,
//                TurnstileResponse = model.TurnstileResponse,
//                SubmittedAt = DateTime.UtcNow
//            };

//            await _submissionService.SubmitAsync(submission);

//            return RedirectToAction("Success");
//        }

//        public IActionResult Test()
//        {
//            return Content("�La acci�n de prueba funciona!");
//        }

//        public IActionResult Success()
//        {
//            return View();
//        }
//    }
//}
