import { StockPrice } from "./stock-price";

export interface Stock {
    index: number,
    id: string,
    name: string,
    industry: string,
    currentPrice: number,
    pricePercentChange: number,
    stockPrices: StockPrice[]
}