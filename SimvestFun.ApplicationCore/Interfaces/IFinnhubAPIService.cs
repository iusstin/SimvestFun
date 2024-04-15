namespace SimvestFun.ApplicationCore.Interfaces
{
    public interface IFinnhubAPIService
    {
        Task<decimal> GetStockPriceAsync(string symbol);
    }
}
