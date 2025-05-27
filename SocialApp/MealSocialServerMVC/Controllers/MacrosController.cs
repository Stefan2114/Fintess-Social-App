
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using AppCommonClasses.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealSocialServerMVC.Controllers
{
    [ApiController]
    [Route("macros")]
    public class MacrosController : Controller
    {
        private readonly IMacrosService _macrosService;

        public MacrosController(IMacrosService macrosService)
        {
            // You may want to use dependency injection for HttpClient in production
            _macrosService = macrosService;
        }
        [HttpGet]
        public IActionResult Macros()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            long userId = long.Parse(userIdString);
            var macros = _macrosService.GetMacrosListByUserId(userId);
            ViewBag.UserId = userId;
            return View(macros);
        }
    }
}
