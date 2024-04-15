using SimvestFun.ApplicationCore.Entities;

namespace SimvestFun.ApplicationCore.Interfaces
{
    public interface ISettingService
    {
        public Task UpdateAnnouncement(string userId, Setting setting);
        public Task<Setting> GetSettingByKey (string key);
    }
}
