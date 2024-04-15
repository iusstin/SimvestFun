namespace SimvestFun.ApplicationCore.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailHash { get; set; }
        public string Token { get; set; }
        public decimal Cash { get; set; }
        public decimal TotalPortfolioValue { get; set; }
        public decimal PortfolioChange { get; set; }
        public string About { get; set; }
        public bool IsAdmin { get; set; }
        public List<UserStockModel> UserStocks { get; set; }
        public string TopInvestments { get; set; }
        public int? YesterdayPosition { get; set; }
        public int? CurrentPosition { get; set; }
    }
}