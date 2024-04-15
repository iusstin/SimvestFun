using Microsoft.EntityFrameworkCore;
using SimvestFun.ApplicationCore.ApplicationExceptions;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.ApplicationCore.Services
{
    public class FollowService : IFollowService
    {
        private readonly ISimvestFunContext _context;
        public FollowService(ISimvestFunContext context)
        {
            _context = context;
        }

        public async Task FollowUserAsync(string loggedUserId, string followedUserId)
        {
            var follower = _context.Follows.Where(f => f.UserId == loggedUserId && f.FollowedUserId == followedUserId).FirstOrDefault();
            if (follower != null)
                throw new InvalidActionException();

            var newFollow = new Follow()
            {
                UserId = loggedUserId,
                FollowedUserId = followedUserId,
            };
            _context.Follows.Add(newFollow);
            await _context.SaveChangesAsync();
        }

        public async Task UnfollowUserAsync(string loggedUserId, string followedUserId)
        {
            var follow = _context.Follows.FirstOrDefault(f => f.UserId == loggedUserId && f.FollowedUserId == followedUserId);
            if (follow == null)
                throw new InvalidActionException();

            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();
        }

        public async Task<Follow?> GetFollowByUserAsync(string loggedUserId, string id)
        {
            return _context.Follows
                .Where(f => f.UserId == loggedUserId && f.FollowedUserId == id).FirstOrDefault();
        }

        public async Task<List<ApplicationUser>> GetAllFollowedUsersAsync(string loggedUserId)
        {
            var followedUsers = await _context.Follows
                .Where(f => f.UserId == loggedUserId)
                .Select(f => f.FollowedUser)
                .Take(9)
                .ToListAsync();
            if(followedUsers.Count > 0)
                followedUsers.Add(await _context.Users.FindAsync(loggedUserId));
    
            followedUsers = followedUsers.OrderByDescending(u => u.TotalPortfolioValue).ToList();

            return followedUsers;
        }

        public async Task<List<ApplicationUser>> GetFollowersByUserAsync(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                throw new EntityNotFoundException();

            var followers = await _context.Follows
                .Where(f => f.FollowedUserId == id)
                .Select(f => f.User)
                .Take(28)
                .ToListAsync();

            return followers;
        }
    }
}