namespace SimvestFun.ApplicationCore.Models
{
    public class StockPriceModel
    {
        public int Id { get; set; }
        public string StockId { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
