using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Entities.External;
using SimvestFun.ApplicationCore.Interfaces;
using SimvestFun.ApplicationCore.Models;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace SimvestFun.ApplicationCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly ISimvestFunContext _context;
        private readonly IPortfolioValuesService _portfolioValuesService;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private const string facebookTokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string facebookUserInfoUrl = "https://graph.facebook.com/me?fields=name,email&access_token={0}";
        private readonly FacebookAuthSettings _facebookAuthSettings;

        public AuthService(ISimvestFunContext context,
                           IJwtUtils jwtUtils,
                           IMapper mapper,
                           UserManager<ApplicationUser> userManager,
                           IPortfolioValuesService portfolioValuesService,
                           IConfiguration configurationProvider)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _userManager = userManager;
            _portfolioValuesService = portfolioValuesService;
            _configuration = configurationProvider;

            _facebookAuthSettings = new FacebookAuthSettings()
            {
                AppId = configurationProvider.GetSection("FacebookCredetials:AppId").Value,
                AppSecret = configurationProvider.GetSection("FacebookCredetials:AppSecret").Value
            };
        }

        public async Task<UserModel> AuthenticateWithPasswordAsync(AuthenticateRequest model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                throw new Exception("Incorrect email or password.");

            var response = _mapper.Map<UserModel>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public async Task RegisterWithPasswordAsync(RegisterRequest model)
        {
            if (_context.Users.FirstOrDefault(u => u.Email == model.Email) != null)
                throw new Exception("There is already an account with this email.");

            var user = _mapper.Map<ApplicationUser>(model);
            user.Cash = 10000m;
            user.TotalPortfolioValue = 10000m;
            user.EmailHash = GetHashEmail(model.Email);
            user.HasBoughtAnyStocks = false;
            user.LastVisitedOn = DateTime.UtcNow;
            user.UnsubscribeGuid = Guid.NewGuid();

            var result = await _userManager.CreateAsync(user, model.Password);
            _context.Users.Add(user);

            await _portfolioValuesService.CreatePortfolioValuesAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> AuthenticateWithGoogleAsync(string idToken)
        {
            var googleUser = await ValidateGoogleAccountAsync(idToken);

            if (googleUser == null)
                throw new Exception("Invalid Google account.");

            try
            {
                var authenticateUserModel = await GenerateTokenAsync(googleUser.Email);
                return authenticateUserModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserModel> RegisterWithGoogleAsync(string idToken)
        {
            var googleUser = await ValidateGoogleAccountAsync(idToken);

            if(googleUser == null)
                throw new Exception("Invalid Google account.");

            try
            {
                await AddNewUserAsync(googleUser.Name, googleUser.Email);
                var authenticateUserModel = await GenerateTokenAsync(googleUser.Email);
                return authenticateUserModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserModel> AuthenticateWithFacebookAsync(string accessToken)
        {
            var tokenIsValid = await ValidateFacebookAccessTokenAsync(accessToken);

            if (tokenIsValid == null || tokenIsValid.Data.IsValid == false)
                throw new Exception("Invalid Facebook account.");

            var facebookUser = await GetFacebookUserInfoAsync(accessToken);

            try
            {
                var authenticateUserModel =  await GenerateTokenAsync(facebookUser.Email);
                return authenticateUserModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserModel> RegisterWithFacebookAsync(string accessToken)
        {
            var tokenIsValid = await ValidateFacebookAccessTokenAsync(accessToken);

            if (tokenIsValid == null || tokenIsValid.Data.IsValid == false)
                throw new Exception("Invalid Facebook account.");

            var facebookUser = await GetFacebookUserInfoAsync(accessToken);

            try
            {
                await AddNewUserAsync(facebookUser.Name, facebookUser.Email);
                var authenticateUserModel = await GenerateTokenAsync(facebookUser.Email);
                return authenticateUserModel;
            }
            catch
            {
                throw;
            }
        }

        private async Task AddNewUserAsync(string name, string email)
        {
            if (_context.Users.FirstOrDefault(u => u.Email == email) != null)
                throw new Exception("There is already an account with this email.");

            var newUser = new ApplicationUser()
            {
                Name = name,
                Email = email,
                Cash = 10000M,
                TotalPortfolioValue = 10000M,
                EmailHash = GetHashEmail(email),
                HasBoughtAnyStocks = false,
                LastVisitedOn = DateTime.UtcNow,
                UnsubscribeGuid = Guid.NewGuid()
            };

            _context.Users.Add(newUser);

            await _portfolioValuesService.CreatePortfolioValuesAsync(newUser);
            await _context.SaveChangesAsync();
        }

        private async Task<UserModel> GenerateTokenAsync(string email)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Email == email);

            if (dbUser == null)
                throw new InvalidCredentialException("You don't have an account yet, please register.");

            var response = _mapper.Map<UserModel>(dbUser);
            response.Token = _jwtUtils.GenerateToken(dbUser);
            return response;
        }

        private string GetHashEmail(string email)
        {
            byte[] hash = MD5.Create().ComputeHash(ASCIIEncoding.ASCII.GetBytes(email.Trim().ToLower()));
            return Convert.ToHexString(hash).ToLower();
        }

        private async Task<GoogleJsonWebSignature.Payload?> ValidateGoogleAccountAsync(string idToken)
        {
            try
            {
                var clientId = _configuration.GetSection("GoogleCredentials:ClientId").Value;
                var googleUser = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings()
                { 
                    Audience = new[] { clientId }
                });

                return googleUser;
            }
            catch
            {
                return null;
            }
        }

        private async Task<FacebookTokenValidationResult?> ValidateFacebookAccessTokenAsync(string accessToken)
        {
            var formatedUrl = string.Format(facebookTokenValidationUrl, accessToken, _facebookAuthSettings.AppId, _facebookAuthSettings.AppSecret);

            var client = new HttpClient();
            HttpRequestMessage UserValidationRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(formatedUrl),
            };

            using (var response = await client.SendAsync(UserValidationRequest))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(body);
            }
        }

        private async Task<RegisterRequest?> GetFacebookUserInfoAsync(string accessToken)
        {
            var formatedUrl = string.Format(facebookUserInfoUrl, accessToken);

            var client = new HttpClient();
            HttpRequestMessage UserInfoRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(formatedUrl),
            };

            using (var response = await client.SendAsync(UserInfoRequest))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RegisterRequest>(body);
                return result;
            }
        }

        public async Task<bool> SendForgotPasswordEmail(string email, string baseUrl)
        {
            if (email == null)
            {
                throw new NotFoundException();
            }

            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);


            if (user == null)
            {
                throw new NotFoundException();
            }

            var resetPasswordGuid = Guid.NewGuid();

            user.ForgotPasswordGuid = resetPasswordGuid;

            var apiKey = _configuration.GetSection("SendGrid:ApiKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("iustin.deaconu@fortech.ro", "Simvest.fun Team");
            var subject = "Simvest.fun Reset Password";
            var to = new EmailAddress(email);
            var plainTextContent = $"Go to this link to reset your password: {baseUrl}reset-password/{resetPasswordGuid}";
            var htmlContent = $"<div>Go to this link to reset your password: {baseUrl}reset-password/{resetPasswordGuid}</div>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            if (response.IsSuccessStatusCode)
                await _context.SaveChangesAsync();

            return response.IsSuccessStatusCode;
        }

        public async Task ResetPassword(ResetPasswordModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ForgotPasswordGuid == model.Guid);

            if(user == null)
            {
                throw new InvalidOperationException();
            }

            await _userManager.CreateAsync(user, model.NewPassword);
            user.ForgotPasswordGuid = null;

            await _context.SaveChangesAsync();
        }
    }
}
