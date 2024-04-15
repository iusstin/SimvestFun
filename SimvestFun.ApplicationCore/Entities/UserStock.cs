namespace SimvestFun.ApplicationCore.Entities
{
    public class UserStock
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string StockId { get; set;}
        public int UnitCount { get; set; }
        public decimal BuyingPricePerUnit { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
