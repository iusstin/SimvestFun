import { Stock } from "./stock";

export interface StockPrice {
    id: number,
    stockId: string,
    price: number,
    timeStamp: Date,
    stock: Stock
}