namespace SimvestFun.ApplicationCore.Entities
{
    public class StockPrice
    { 
        public int Id { get; set; }
        public string StockId { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeStamp { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
