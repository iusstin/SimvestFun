using SimvestFun.ApplicationCore.Entities;

namespace SimvestFun.ApplicationCore.Interfaces
{
    public interface IStockService
    {
        Task<List<Stock>> GetAllStocksAsync();
        Task<bool> UpdateStockPricesAsync(bool dayChanged);
        Task<Stock> GetStockWithPricesAsync(string id);
        Task<bool> CheckTwelveHourChange();
    }
}
