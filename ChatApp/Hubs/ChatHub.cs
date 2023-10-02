using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public ChatHub(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task SendMessage(string userId, string message)
        {
            var appUser = await _userManager.FindByIdAsync(userId);

            if (appUser != null)
            {
                if (!string.IsNullOrWhiteSpace(appUser.ConnectionId))
                {
                    string user = _contextAccessor.HttpContext.User.Identity.Name;

                    await Clients.Client(appUser.ConnectionId).SendAsync("ReceiveMessage", user, message);
                }
            }
        }

        public async override Task OnConnectedAsync()
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);

                user.ConnectionId = Context.ConnectionId;
                await _userManager.UpdateAsync(user);

                string userId = user.Id;
                await Clients.All.SendAsync("Loggin", userId);
            }
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);

                user.ConnectionId = null;
                await _userManager.UpdateAsync(user);

                string userId = user.Id;
                await Clients.All.SendAsync("Logout", userId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
