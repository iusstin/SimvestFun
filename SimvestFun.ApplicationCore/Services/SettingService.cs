using Microsoft.EntityFrameworkCore;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.ApplicationCore.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISimvestFunContext _context;
        public SettingService(ISimvestFunContext context)
        {
            _context = context;
        }

        public async Task<Setting?> GetSettingByKey(string key)
        { 
            return await _context.Settings.FirstOrDefaultAsync(s => s.Key == key);
        }

        public async Task UpdateAnnouncement(string userId, Setting announcement)
        {
            var connectedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (!connectedUser.IsAdmin)
                throw new UnauthorizedAccessException();

            var dbAnnouncement = await _context.Settings.FirstOrDefaultAsync(s => s.Key.ToLower() == "announcement");

            if (dbAnnouncement != null)
            {
                dbAnnouncement.Value = announcement.Value;
            }
            else
            {
                await _context.Settings.AddAsync(announcement);
            }

            await _context.SaveChangesAsync();
        }
    }
}
