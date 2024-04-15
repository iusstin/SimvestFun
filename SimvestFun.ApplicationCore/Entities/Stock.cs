namespace SimvestFun.ApplicationCore.Entities
{
    public class Stock
    {
        public int Index { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public string Industry { get; set; }
        public DateTime PriceUpdatedOn { get; set; }
        public decimal PricePercentChange { get; set; } = 0M;
        public decimal YesterdayPrice { get; set; }
        public List<StockPrice> StockPrices { get; set; }
    }
}
