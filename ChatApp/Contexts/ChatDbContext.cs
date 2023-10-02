using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Contexts
{
    public class ChatDbContext : IdentityDbContext<AppUser>
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
            
        }
    }
}
