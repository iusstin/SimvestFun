using SimvestFun.ApplicationCore.Entities;

namespace SimvestFun.ApplicationCore.Interfaces
{
    public interface IFollowService
    {
        Task FollowUserAsync(string loggedUserId, string id);
        Task UnfollowUserAsync(string loggedUserId, string id);
        Task<Follow> GetFollowByUserAsync(string loggedUserId, string id);
        Task<List<ApplicationUser>> GetAllFollowedUsersAsync(string id);
        Task<List<ApplicationUser>> GetFollowersByUserAsync(string id);
    }
}