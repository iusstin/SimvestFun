namespace SimvestFun.ApplicationCore.Interfaces
{
    public interface IStockPriceService
    {
        Task SaveYesterdayStockPricesAsync();
        Task<decimal> GetYesterdayPriceAsync(string stockId);
    }
}
