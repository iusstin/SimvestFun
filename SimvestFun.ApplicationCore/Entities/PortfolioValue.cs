namespace SimvestFun.ApplicationCore.Entities
{
    public class PortfolioValue
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public decimal TotalPortfolioValue  { get; set; }
        public DateTime TimeStamp { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
