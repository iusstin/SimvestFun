using SimvestFun.ApplicationCore.Models;

namespace SimvestFun.ApplicationCore.Interfaces
{
    public interface IAuthService
    {
        Task<UserModel> AuthenticateWithPasswordAsync(AuthenticateRequest model);
        Task RegisterWithPasswordAsync(RegisterRequest model);
        Task<UserModel> AuthenticateWithGoogleAsync(string idToken);
        Task<UserModel> RegisterWithGoogleAsync(string idToken);
        Task<UserModel> AuthenticateWithFacebookAsync(string authToken);
        Task<UserModel> RegisterWithFacebookAsync(string authToken);
        Task<bool> SendForgotPasswordEmail(string email, string baseUrl);
        Task ResetPassword(ResetPasswordModel model);
    }
}
