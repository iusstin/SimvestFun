using Microsoft.AspNetCore.Identity;

namespace SimvestFun.ApplicationCore.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public decimal Cash { get; set; } = 10000M;
        public string EmailHash { get; set; }
        public decimal TotalPortfolioValue { get; set; } = 10000M;
        public decimal PortfolioChange { get; set; } = 0M;
        public decimal YesterdayPortfolioValue { get; set; } = 0M;
        public string About { get; set; }
        public bool IsAdmin { get; set; }
        public List<UserStock> UserStocks { get; set; } = new List<UserStock>();
        public string? TopInvestments { get; set; } = null;
        public List<PortfolioValue> PortfolioValues { get; set; }
        public int? YesterdayPosition { get; set; } = null;
        public int? CurrentPosition { get; set; } = null;
        public bool HasBoughtAnyStocks { get; set; }
        public DateTime LastVisitedOn { get; set; } = new DateTime(2022, 5, 1, 0, 0, 0);
        public DateTime? LastEmailSentOn { get; set; } = null;
        public bool IsUnsubscribedFromEmails { get; set; } = false;
        public Guid UnsubscribeGuid { get; set; }
        public Guid? ForgotPasswordGuid { get; set; }
    }
}
