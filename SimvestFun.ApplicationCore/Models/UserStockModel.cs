namespace SimvestFun.ApplicationCore.Models
{
    public class UserStockModel
    {
        public string ApplicationUserId { get; set; }
        public string StockId { get; set; }
        public int UnitCount { get; set; }
        public decimal BuyingPricePerUnit { get; set; }
    }
}
