using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Loading.Models;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;

namespace Loading.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IHubContext<ChatHub> _chatHubContext;
        public HomeController(IHubContext<ChatHub> chatHubContext, ILogger<HomeController> logger)
        {
            _chatHubContext = chatHubContext;
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Processing() {
            var vm = new ProcessingViewModel();
            
            for (int i = 0; i < 1000; i++) {
                var perc = vm.Percentage;
                vm.Percentage = (i * 100) / 1000;
                if(perc != vm.Percentage)
                    _chatHubContext.Clients.All.SendAsync("ReceivePercent", "percent: ", vm.Percentage);
            }
            return Ok();
        }

        public IActionResult SendMessage(string user, string message) {
            _chatHubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
            return Ok();        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
