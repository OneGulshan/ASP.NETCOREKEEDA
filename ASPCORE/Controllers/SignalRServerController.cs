using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ASPCORE.Models;

namespace ASPCORE.Controllers
{
    public class SignalRServerController : Controller
    {
        private readonly IHubContext<NotificationHub> _notificationHub;

        public SignalRServerController(IHubContext<NotificationHub> notificationHub)
        {
            _notificationHub = notificationHub;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.NumberOfClientCount = ConnectedUsers.UserId?.Count;
            ViewBag.NumberOfClientUserId = ConnectedUsers.UserId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Notification model)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await _notificationHub.Clients.All.SendAsync("ReceiveMsg", model.Message);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return View();
        }
    }
}
