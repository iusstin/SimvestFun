using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    public partial class GroupUserStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            string groupUserStocksByStockIdScript = @"
                declare @local_UserStocks table
	            (Id int primary key identity,
	             StockId nvarchar(450),
	             UnitCount int,
	             ApplicationUserId nvarchar(450),
	             WeightedMean decimal(18,2)
	            );

	            insert into @local_UserStocks(StockId, UnitCount, ApplicationuserId, WeightedMean)
	            (select StockId, sum(uc) as UnitCount, ApplicationUserId, sum(bpu * uc) /sum(uc) as WeightedMean
                from (select BuyingPricePerUnit as bpu, UnitCount as uc, Id from UserStocks us1
                where us1.StockId = StockId and us1.ApplicationUserId = ApplicationUserId) src
	            inner join UserStocks us on src.Id = us.Id
                group by StockId, ApplicationUserId );

	            declare @usCount int;
	            set @usCount = (select count(*) from UserStocks);

                insert into UserStocks(ApplicationUserId, StockId, UnitCount, BuyingPricePerUnit)
                select ApplicationUserId, StockId, UnitCount, FORMAT(WeightedMean, '###.##') as BuyingPricePerUnit 
	            from @local_UserStocks;

                delete from UserStocks where Id IN (select top(@usCount) Id from UserStocks order by Id);
            ";

            migrationBuilder.Sql(groupUserStocksByStockIdScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
