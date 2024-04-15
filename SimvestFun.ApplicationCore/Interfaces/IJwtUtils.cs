using SimvestFun.ApplicationCore.Entities;

namespace SimvestFun.ApplicationCore.Interfaces
{
    public interface IJwtUtils
    {
        public string GenerateToken(ApplicationUser user);
        public string? ValidateToken(string token);
    }
}